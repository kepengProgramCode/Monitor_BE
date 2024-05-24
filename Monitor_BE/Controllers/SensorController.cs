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
    public class SensorController : ControllerBase
    {
        private readonly Sensor_Class_Service sensor_class;
        private readonly Sensor_Type_Service sensor_type;
        private readonly LogService logger;

        public SensorController(Sensor_Class_Service _sensor_class, Sensor_Type_Service _sensor_type, LogService _logService)
        {
            sensor_class = _sensor_class;
            sensor_type = _sensor_type;
            logger = _logService;
        }

        #region 查询

        /// <summary>
        /// 获取设备类列表
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [HttpPost("GetSensorClassList")]
        [AllowAnonymous]
        public ResponseResult<ResLists<tb_sensor_class>>? GetSensorClassList(GetrPar<string> sensorPar) => sensor_class.GetSensorClassList(sensorPar);

        /// <summary>
        /// 获取设备型号列表
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [HttpPost("GetSensorTypeList")]
        [AllowAnonymous]
        public ResponseResult<ResLists<tb_sensor_type>>? GetSensorTypeList(GetrPar<string> sensorPar) => sensor_type.GetSensorTypeList(sensorPar);

        #endregion


        #region 增加
        /// <summary>
        /// 增加设备类列表
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [HttpPost("AddSensorClassList")]
        [AllowAnonymous]
        public ResponseResult<int> AddSensorClassList(tb_sensor_class ts)
        {
            return sensor_class.RegistSensorClass(ts, logger);
        }

        /// <summary>
        /// 增加设备型号列表
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        [HttpPost("AddSensorTypeList")]
        [AllowAnonymous]
        public ResponseResult<int> AddSensorTypeList(tb_sensor_type ts)
        {
            return sensor_type.RegistSensorType(ts, logger);
        }

        #endregion
    }
}
