using SqlSugar;

namespace Monitor_BE.Repository
{
    public class AccessClient<T> : SimpleClient<T> where T : class, new()
    {
        private ISqlSugarClient db;
        private static string dbconnection;
        public ISqlSugarClient Db { get => db; }

        static AccessClient()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            dbconnection = config.GetConnectionString("Dbconnection");
        }

        public AccessClient(ISqlSugarClient? contex = null) : base(contex)
        {
            if (contex == null)
            {
                //这里将SqlSuggerClient换成SqlScop
                Context = new SqlSugarScope(new ConnectionConfig()
                {
                    ConnectionString = dbconnection,
                    DbType = DbType.MySql,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                },
                lg =>
                {
                    lg.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响
                    };
                });
            }
            db = Context;
        }
    }
}
