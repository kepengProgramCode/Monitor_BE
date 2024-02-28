using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;
using Monitor_BE.ServiceBuiness;

namespace Monitor_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ApiControllerBase
    {
        private readonly RoleService role;
        private readonly LogService logger;
        public RoleController(RoleService _role, LogService _logService)
        {
            this.role = _role;
            logger = _logService;
        }

        protected override void OnWriteError(string action, string message)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取当前菜单，根据用户名
        /// 在此处使用前端框架已经封装好了独立参数，不需要增加二外的[FromBody]等参数
        /// </summary>
        /// <returns></returns>
        [HttpGet("findRole")]
        [AllowAnonymous]
        public ResponseResult<IEnumerable<tb_role>>? findRole(string type)
        {
            var data = role.findRole(type);
            return data;
        }

    }
}
