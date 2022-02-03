using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ReverseProxy.Store.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReverseProxy.Store.Distributed.JWT
{
    public static class JWTHelper
    {
        /// <summary>
        /// 生成JWT
        /// </summary>
        /// <param name="user"></param>
        /// <param name="_jwtSettings"></param>
        /// <returns></returns>
        public static async Task<string> CreateToken(User user, JwtSettings _jwtSettings, IDatabase redis)
        {
            var uuid = Guid.NewGuid().ToString("N");
            await SaveUserDetail(user, uuid, redis);
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var authTime = DateTime.Now;//授权时间
            var expiresAt = authTime.AddDays(30);//过期时间
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtClaimTypes.Audience,_jwtSettings.Audience),
                    new Claim(JwtClaimTypes.Issuer,_jwtSettings.Issuer),
                    new Claim(JwtClaimTypes.Id, uuid),
                }),
                Expires = expiresAt,
                //对称秘钥SymmetricSecurityKey
                //签名证书(秘钥，加密算法)SecurityAlgorithms
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="_jwtSettings"></param>
        /// <returns></returns>
        public static string RefrashToken(User user, JwtSettings _jwtSettings)
        {

            return "";
        }

        private static async Task SaveUserDetail(User user, string uuid, IDatabase redis)
        {
            await redis.StringSetAsync(uuid, JsonConvert.SerializeObject(user));
        }
        public static async Task<User?> GetUserDetail(string token, IDatabase redis)
        {
            var uuid = GetUUID(token, redis);
            var json = (await redis.StringGetAsync(uuid)).ToString() ?? "";
            return JsonConvert.DeserializeObject<User>(json ?? "");
        }

        public static string GetUUID(string token, IDatabase redis)
        {
            var x = new JwtSecurityToken(token);
            var uuid = x.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id).ToString() ?? "";
            return uuid.Replace("id: ", "");
        }
    }
}
