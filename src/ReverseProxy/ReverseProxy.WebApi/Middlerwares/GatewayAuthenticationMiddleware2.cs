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
    public class GatewayAuthenticationMiddleware2
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IDatabase _redis;

        public GatewayAuthenticationMiddleware2(
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
            await _next(context);
            return;
        }
    }
}
