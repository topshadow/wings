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

    public class LoginForm {
        public string username { get; set; }
        public string password { get; set; }
    }

    /// <summary>
    /// 登录注册模块
    /// </summary>
    [Route ("api/Auth/[controller]")]
    [ApiController]
    public class SignController : ControllerBase {
        private readonly UserManager<User> _userManage;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RBACContext rbacContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_rbacContext"></param>
        public SignController(RBACContext _rbacContext) { this.rbacContext = _rbacContext; }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost ("[action]")]
        public object login ([FromBody] LoginForm loginForm) {
            var payload = new Dictionary<string, object> { { "claim1", 0 },
                    { "claim2", "claim2-value" }
                };
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            var userExist = (from user in this.rbacContext.Users where user.username == loginForm.username select user).First();
            if (userExist!=null)
            {
                var roles = (from role in this.rbacContext.Roles where userExist.roleIds.Contains(role.roleId.ToString()) select role).ToList();
                var menuIds = new List<int>();
                roles.ForEach(role =>
                {
                    var idArr=role.menuIds.Split(",").Where(idStr => idStr != "");
                    
                    idArr.ToList().ForEach(id =>
                    {
                        if (!menuIds.Contains(int.Parse(id)))
                        {
                            menuIds.Add(int.Parse(id));
                        }
                    });
                });
                var menus = (from menu in this.rbacContext.Menus where menuIds.Contains(menu.menuId) select menu).ToList();
              
                return new { isSuccess=true,user=userExist,roles=roles.ToList(),menuIds=menuIds,menus=menus };
            }
            else
            {
                
                return new { isSuccess = false,errMsg="用户不存在" };
            }
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm ();
            IJsonSerializer serializer = new JsonNetSerializer ();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder ();
            IJwtEncoder encoder = new JwtEncoder (algorithm, serializer, urlEncoder);

            var token = encoder.Encode (payload, secret);
            Console.WriteLine (token);
            return new { token=token};
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