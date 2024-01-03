using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;

namespace Monitor_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class MenuController : ControllerBase
    {
        private readonly MenuService menu;
        private readonly LogService log;

        public MenuController(MenuService _menu, LogService _log)
        {
            menu = _menu;
            log = _log;
        }


        /// <summary>
        /// 获取资产类型
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("GetFindMenu")]
        public ResponseResult<IEnumerable<tb_menu>>? GetFindMenu(string u_name)
        {
            var data = menu.findTree(u_name, 1);
            return data.ToList();
        }
    }
}
