using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Api;
using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;
using Monitor_BE.ServiceBuiness;

namespace Monitor_BE.Controllers
{
    [ApiFilter]
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly UserService users;
        private readonly LogService logger;

        public ManagerController(UserService _users, LogService _logService)
        {
            users = _users;
            logger = _logService;
        }

    }
}
