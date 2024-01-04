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


        [HttpGet("GetFindMenu")]
        [AllowAnonymous]
        public ResponseResult<IEnumerable<tb_menu>>? GetFindMenu()
        {
            var data = menu.GetRouteHierarchyAsync();
            return data.Result;
        }



        /// <summary>
        /// 获取资产类型
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("GetFindPermission")]
        [AllowAnonymous]
        public ResponseResult<IEnumerable<tb_role_permissions>>? GetFindPermission()
        {
            var data = menu.findPermission();
            return data;
        }

        protected override void OnWriteError(string action, string message)
        {

        }
    }
}
