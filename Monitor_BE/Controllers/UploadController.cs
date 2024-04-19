using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;
using Monitor_BE.ServiceBuiness;
using NewLife.Log;

namespace Monitor_BE.Controllers
{
    [Route("file")]
    [ApiController]
    public class UploadController : ApiControllerBase
    {
        private readonly UploadService _upload;
        private readonly LogService logger;

        public UploadController(UploadService _upload, LogService _logService)
        {
            this._upload = _upload;
            logger = _logService;
        }

        protected override void OnWriteError(string action, string message)
        {
            XTrace.WriteLine("UserController服务信息：{0}-------{1}", action, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UploadImage")]
        public ResponseResult<string>? UploadImage(object img)
        {
            return "new ResponseResult<LogoutResponse>();";
        }


    }
}
