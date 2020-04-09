using Camefor.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Camefor.Controllers {



    /// <summary>
    /// 登录 认证 授权
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OnAuthController : ControllerBase {


        private readonly ILogger<OnAuthController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public OnAuthController(ILogger<OnAuthController> logger) {
            _logger = logger;
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Get(string username, string password) {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                return new JsonResult(new {
                    Status = false,
                    message = "用户名或密码不能为空"
                });
            }
            TokenModelJWT tokenModel = new TokenModelJWT();
            tokenModel.Uid = 1;
            tokenModel.Role = "Admin";

            var jwt = JwtHelper.IssueJWT(tokenModel);
            suc = true;

            return Ok(new {
                success = suc,
                token = jwt[0],
                validFrom = TimeZone.CurrentTimeZone.ToLocalTime(Convert.ToDateTime(jwt[1])).ToString(),
                expiration = TimeZone.CurrentTimeZone.ToLocalTime(Convert.ToDateTime(jwt[2])).ToString()
            });
        }

    }
}