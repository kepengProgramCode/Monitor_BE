﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common;
using Monitor_BE.Common.Response;
using Monitor_BE.Common.Token;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;
using Monitor_BE.ServiceBuiness;
using NewLife; 
using NewLife.Log;
using NewLife.Security;
using System.Reflection;
using System.Xml;

namespace Monitor_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly UserService users;
        private readonly LogService logger;

        public UserController(UserService _users, LogService _logService)
        {
            this.users = _users;
            logger = _logService;
        }
        protected override void OnWriteError(string action, string message)
        {
            XTrace.WriteLine("UserController服务信息：{0}-------{1}", action, message);
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public ResponseResult<LoginResponse>? Login(LoginEntity user)
        {
            //生成实体表结构
            //users.Db.DbFirst.IsCreateAttribute().CreateClassFile(@"C:\Users\Peng\Desktop\Monitor_BE", "Monitor_BE.Entity");

            var data = users.GetUserList(new tb_user() { u_name = user.Username, u_pwd = user.Password });
            //string md5s = Fun.md5Encrypt(user.Username);
            if (data == null || data.Count < 1) return null;
            tb_user tb_User = data.First();
            var token = TokenAuth.IssueToken(Rand.NextString(8));

            tb_token tb_Token = new()
            {
                token_str = token.AccessToken,
                u_id = tb_User.u_id,
            };
            tb_User.u_token = token.AccessToken;
            users.UpdateUser(tb_User, logger);
            users.UpdateUserToken(tb_Token, logger);

            LoginResponse loginResponse = new()
            {
                //    //Role = user.Username == "keke" ? 1 : 2,
                access_token = token.RefreshToken,
                //    //Auth = user.Username == "keke" ? new string[] { "ADD", "DELETE", "DETAILS" } : new string[] { "DETAILS" }
            };
            return loginResponse;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        [AllowAnonymous]
        public ResponseResult<LogoutResponse>? Logout() => new ResponseResult<LogoutResponse>();


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [HttpPost("GetUsers")]
        [AllowAnonymous]
        public ResponseResult<ResLists<tb_user>>? GetUsers(GetrPar<string> userPar) => users.GetUserList(userPar);



        /// <summary>
        /// 获取状态列表
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [HttpGet("GetUsersStatus")]
        [AllowAnonymous]
        public ResponseResult<IEnumerable<Status>>? GetUsersStatus()
        {
            return new List<Status>()
            {
                new Status()
                {
                    userLabel= "启用",
                    userStatus= 1,
                    tagType= "success"
                },
                new Status()
                {

                    userLabel= "禁用",
                    userStatus= 0,
                    tagType= "danger"
                }
            };
        }



        [HttpPost("Test4")]
        public ResponseResult<string> Test4([FromBody] string tt)
        {
            return tt;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        //[Authorize(Roles = "Admin")]
        public ResponseResult<int> RegisterUser(tb_user user)
        {
            user.createTime = DateTime.Now.ToFullString();
            user.status = 1;
            user.u_type = 1;
            string? name = MethodBase.GetCurrentMethod()?.DeclaringType?.Name;
            string? method = MethodBase.GetCurrentMethod()?.Name;
            logger.Info(name + method);
            logger.Info($"用户{user.u_name} 被创建");
            user.u_pwd = Fun.md5Encrypt(user.u_pwd);
            return users.RegisterUser(user, logger);
        }

        [HttpPost("UpdateUser")]
        //[Authorize(Roles = "Admin")]
        public ResponseResult<int> UpdateUser(tb_user user)
        {
            return users.UpdateUser(user, logger);
        }

        [AllowAnonymous]
        [HttpPost("UpdateUserStatus")]
        public ResponseResult<int> UpdateUserStatus(int u_id, int status)
        {
            return users.UpdateStatus([u_id, status]);
        }

        [HttpPost("DeleteUser")]
        //[Authorize(Roles = "Admin")]
        public ResponseResult<int> DeleteUser(tb_user user)
        {
            return users.DeleteUser(user, logger);
        }


    }
}
