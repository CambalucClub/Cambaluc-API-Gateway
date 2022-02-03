namespace ReverseProxy.WebApi.Middlerwares
{
    /// <summary>
    /// 网关中间件扩展方法
    /// </summary>
    public static class GatewayMiddlewareExtensions
    {
        /// <summary>
        /// 使用网关认证授权中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GatewayAuthenticationMiddleware>();
        }
    }
}
