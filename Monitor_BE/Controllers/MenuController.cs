using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;

namespace Monitor_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class MenuController : ApiControllerBase
    {
        private readonly MenuService menu;
        private readonly LogService log;

        public MenuController(MenuService _menu, LogService _log)
        {
            menu = _menu;
            log = _log;
        }

        /// <summary>
        /// 获取当前菜单，根据用户名
        /// 在此处使用前端框架已经封装好了独立参数，不需要增加二外的[FromBody]等参数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFindMenu")]
        [AllowAnonymous]
        public ResponseResult<IEnumerable<tb_menu>>? GetFindMenu(string u_id)
        {
            var data = menu.GetRouteHierarchyAsync(u_id);
            return data.Result;
        }





        /// <summary>
        /// 获取菜单权限类型
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("GetFindPermission")]
        [AllowAnonymous]
        public ResponseResult<IEnumerable<tb_role_permissions>>? GetFindPermission(string u_id)
        {
            var data = menu.findPermission(u_id);
            return data;
        }

        protected override void OnWriteError(string action, string message)
        {

        }
    }
}
