using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wings.Framework.Auth.Bean;
using Wings.Framework.RBAC.Model;
namespace Wings.Framework.Auth.Controller {
    /// <summary>
    /// 登录注册模块
    /// </summary>
    [Route ("api/sign/[controller]")]
    [ApiController]
    public class SignController : ControllerBase {
        private readonly UserManager<User> _userManage;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet ("login")]
        public string login () {
            var payload = new Dictionary<string, object> { { "claim1", 0 },
                    { "claim2", "claim2-value" }
                };
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm ();
            IJsonSerializer serializer = new JsonNetSerializer ();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder ();
            IJwtEncoder encoder = new JwtEncoder (algorithm, serializer, urlEncoder);

            var token = encoder.Encode (payload, secret);
            Console.WriteLine (token);
            return token;
        }
        /// <summary>
        /// 获取登录token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet ("myToken")]
        public string myToken (string token) {
            try {
                var json = new JwtBuilder ()
                    .Decode (token);
                Console.WriteLine (json);
                return json;
            } catch (TokenExpiredException) {
                Console.WriteLine ("Token has expired");
                return "";
            } catch (SignatureVerificationException) {
                Console.WriteLine ("Token has invalid signature");
                return "none";
            }
        }
    }
}