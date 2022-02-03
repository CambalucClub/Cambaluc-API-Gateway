using ReverseProxy.WebApi.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using ReverseProxy.Store.Entity;
using ReverseProxy.Store.EFCore;
using Microsoft.EntityFrameworkCore;
using ReverseProxy.Store.EFCore.Management;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using Yarp.ReverseProxy.Model;
using ReverseProxy.Store.Distributed.Redis;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using ReverseProxy.Store.Distributed.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using System.Text;
using ReverseProxy.WebApi.Middlerwares;
using ReverseProxy.WebApi.Permission;

const string OUTPUT_TEMPLATE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} <{ThreadId}> [{Level:u3}] {Message:lj}{NewLine}{Exception}";
var builder = WebApplication.CreateBuilder(args);

//ConfigurationManager ºÍ IWebHostEnvironment
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

//settings
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


// Logging  1.去掉默认添加的日志提供程序  2.使用serilog日志提供程序
builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostBulderContext, loggerConfiguration) =>
{
    loggerConfiguration
    .WriteTo.Console(outputTemplate: OUTPUT_TEMPLATE)
    .WriteTo.File("logs/app.txt"
        , rollingInterval: RollingInterval.Day
        , outputTemplate: OUTPUT_TEMPLATE);
});
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddMemoryCache();
// 添加验证器
builder.Services.AddSingleton<IValidator<Cluster>, ClusterValidator>();
builder.Services.AddSingleton<IValidator<ProxyRoute>, ProxyRouteValidator>();



string UseConnection = "MySQL";
UseConnection = builder.Configuration.GetConnectionString("UseConnection");

switch (UseConnection)
{
    case "MySQL":
        builder.Services.AddDbContext<EFCoreDbContext>(options =>
                        options.UseMySql(
                            builder.Configuration.GetConnectionString("MySQL"),
                            ServerVersion.Parse("10.6.5-mariadb"))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors());
        break;
    case "MSSQL":
        builder.Services.AddDbContext<EFCoreDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("MSSQL"),
                    b => b.MigrationsAssembly("ReverseProxy.WebApi")));
        break;
}


builder.Services.AddTransient<IClusterManagement, ClusterManagement>();
builder.Services.AddTransient<IProxyRouteManagement, ProxyRouteManagement>();
builder.Services.AddControllers()
    .AddFluentValidation()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    }); ;

builder.Services.AddReverseProxy()
    .LoadFromEFCore()
    .AddRedis(configuration.GetConnectionString("Redis"));
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "汗八里API网关（Cambaluc-API-Gateway）", Version = "v1" });
});


#region Jwt配置
//将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

//由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
//将配置绑定到JwtSettings实例中
var jwtSettings = new JwtSettings();
configuration.Bind("JwtSettings", jwtSettings);

//添加身份验证
builder.Services
.AddAuthorization(options =>
{
    options.AddPolicy("permission", policy =>
        policy.RequireAuthenticatedUser());
})
.AddAuthentication(options =>
{
    //认证middleware配置
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    //jwt token参数设置
    o.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = JwtClaimTypes.Name,
        RoleClaimType = JwtClaimTypes.Role,
        //Token颁发机构
        ValidIssuer = jwtSettings.Issuer,
        //颁发给谁
        ValidAudience = jwtSettings.Audience,
        //这里的key要进行加密
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "汗八里API网关（Cambaluc-API-Gateway） v1"));
}

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        //.AllowCredentials()
        ;
});
app.UseRouting();
app.UseAuthentication();
////使用网关认证授权中间件
//app.UseGatewayAuthenticationMiddleware();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapReverseProxy(proxyPipeline =>
    {
        // Custom endpoint selection
        proxyPipeline.Use((context, next) =>
        {
            var req = context.Request;
            Log.Information($"Method:{req.Method},Url:{req.Host}{req.Path}");
            return next();
        });
        //使用网关认证授权中间件
        proxyPipeline.UseGatewayAuthenticationMiddleware();
        proxyPipeline.UseSessionAffinity();
        proxyPipeline.UseLoadBalancing();
        proxyPipeline.UsePassiveHealthChecks();
    })
   .ConfigureEndpoints((builder, route) => builder.WithDisplayName($"ReverseProxy {route.RouteId}-{route.ClusterId}"));
});

app.Run();