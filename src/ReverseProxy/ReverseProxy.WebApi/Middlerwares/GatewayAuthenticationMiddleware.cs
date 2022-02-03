using Newtonsoft.Json;
using ReverseProxy.Store.Distributed.JWT;
using StackExchange.Redis;
using System.Net;
using Yarp.ReverseProxy.Model;

namespace ReverseProxy.WebApi.Middlerwares
{
    /// <summary>
    /// 网关认证授权中间件
    /// </summary>
    public class GatewayAuthenticationMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IDatabase _redis;

        public GatewayAuthenticationMiddleware(
            RequestDelegate next,
            ILogger<GatewayAuthenticationMiddleware> logger,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _redis = connectionMultiplexer.GetDatabase(0);
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint()
                ?? throw new InvalidOperationException($"Routing Endpoint wasn't set for the current request.");

            var route = endpoint.Metadata.GetMetadata<RouteModel>()
                ?? throw new InvalidOperationException($"Routing Endpoint is missing {typeof(RouteModel).FullName} metadata.");


            var cluster = route.Cluster ?? throw new InvalidOperationException($"Route  has no cluster information");


            //网关授权策略[AuthorizationPolicy] 不配置，则匿名直接访问
            if (string.IsNullOrWhiteSpace(route.Config.AuthorizationPolicy))
            {
                await _next(context);
                return;
            }

            //需要认证授权
            var (flag, msg) = await GetAuthenticationResult(context, route, cluster);
            if (flag)
            {
                await _next(context);
                return;
            }

            await AuthFailed(context, msg);
        }

        /// <summary>
        /// 认证授权
        /// </summary>
        /// <param name="context"></param>
        private async Task<(bool flag, string? msg)> GetAuthenticationResult(HttpContext context, RouteModel route, ClusterState cluster)
        {
            //没有token无权限访问
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrWhiteSpace(token))
                return (false, "您无权访问！");

            //获取yarp路由规则
            //如果yarp路由规则包含{**catch-all}，则去掉{**catch-all}再和当前请求链接匹配，不匹配则无权访问
            var requestPath = context.Request.Path.Value?.ToLower() ?? "";
            var matchPath = route.Config.Match.Path?.ToLower() ?? "";
            if (matchPath.Contains("/{**catch-all}"))
            {
                matchPath = matchPath.Replace("{**catch-all}", "");
                if (!requestPath.Contains(matchPath))
                    return (false, "您无权访问当前链接！");
            }
            else
            {
                if (matchPath != requestPath)
                    return (false, "您无权访问当前链接！");
            }



            var user = await JWTHelper.GetUserDetail(token, _redis);
            if (user == null || !user.Permission.Contains(requestPath))
                return (false, "您无权访问当前链接！");

            return (true, null);

        }

        public async Task AuthFailed(HttpContext context, string? msg)
        {
            using (Stream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(
                JsonConvert.SerializeObject(msg))))
            {
                await stream.CopyToAsync(context.Response.Body);
            }
        }
    }
}
