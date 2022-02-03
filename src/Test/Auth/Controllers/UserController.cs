using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReverseProxy.Store.Distributed.JWT;
using ReverseProxy.Store.Entities;
using StackExchange.Redis;

namespace Auth.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {        //获取JwtSettings对象信息
        private readonly JwtSettings _jwtSettings;
        private readonly IDatabase _redis;

        public UserController(
            IOptions<JwtSettings> jwtSettings,
            IConnectionMultiplexer connectionMultiplexer
            )
        {
            _jwtSettings = jwtSettings.Value;
            _redis = connectionMultiplexer.GetDatabase(0);
        }


        [Route("Login")]
        [HttpGet]
        public async Task<string> Login()
        {
            //测试自己创建的对象
            var user = new User
            {
                Id = 1,
                Phone = "155XXXXXXXX",
                Role = new List<string>
                {
                    "User"
                },
                Permission = new List<string>
                {
                    "/api01/weatherforecast",
                    "/api02/weatherforecast2",
                }
            };
            var token = await JWTHelper.CreateToken(user, _jwtSettings, _redis);

            return token;
        }

        [Route("GetUser")]
        [HttpPost]
        public async Task<string> GetUserInfo()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = await JWTHelper.GetUserDetail(token, _redis);
            if (user == null)
                return "user is null";
            return JsonConvert.SerializeObject(user);
        }
    }
}
