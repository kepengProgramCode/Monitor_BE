using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.Repository;

namespace Monitor_BE.ServerBusiness
{
    public partial class Sensor_Class_Service : AccessClient<tb_sensor_class>
    {
        public ResLists<tb_sensor_class>? GetSensorClassList(GetrPar<string> seneorPar)
        {
            var source = Db.Queryable<tb_sensor_class>().ToList();
            if (!string.IsNullOrEmpty(seneorPar.dynamicParams))
            {
                source = source.FindAll((o) => o.sensor_class.Contains(seneorPar.dynamicParams));
            }
            ResLists<tb_sensor_class> list = new()
            {
                list = source,
                pageNum = seneorPar.pageNum,
                pageSize = seneorPar.pageSize
            };
            return list;
        }


        public int RegistSensorClass(tb_sensor_class sensor, LogService log)
        {
            int res = 0;
            try
            {
                sensor.sensor_class = sensor.sensor_class.ToUpper();
                Db.Ado.BeginTran();
                res = Db.Insertable(sensor).ExecuteCommand() > 0 ? 0 : 1;
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                log.Error("注册用户错误", ex);
            }
            return res;
        }
    }

    public class Sensor_Type_Service : AccessClient<tb_sensor_type> 
    {
        public ResLists<tb_sensor_type>? GetSensorTypeList(GetrPar<string> sensorPar)
        {
            var source = Db.Queryable<tb_sensor_type>().ToList();
            if (!string.IsNullOrEmpty(sensorPar.dynamicParams))
            {
                source = source.FindAll((o) => o.sensor_class.Contains(sensorPar.dynamicParams));
            }
            ResLists<tb_sensor_type> list = new()
            {
                list = source,
                pageNum = sensorPar.pageNum,
                pageSize = sensorPar.pageSize
            };
            return list;
        }


        public int RegistSensorType(tb_sensor_type sensor, LogService log)
        {
            int res = 0;
            try
            {
                sensor.sensor_class.ToUpper();
                Db.Ado.BeginTran();
                res = Db.Insertable(sensor).ExecuteCommand() > 0 ? 0 : 1;
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                log.Error("注册用户错误", ex);
            }
            return res;
        }
    }
}