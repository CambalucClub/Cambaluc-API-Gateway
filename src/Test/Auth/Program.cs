using ReverseProxy.Store.Distributed.JWT;
using ReverseProxy.Store.Distributed.Redis;
using StackExchange.Redis;
using Yarp.ReverseProxy.Store;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
var app = builder.Build();


app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
