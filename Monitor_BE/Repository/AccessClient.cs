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
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = dbconnection,
                    DbType = DbType.MySql,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                });
            }
            db = base.Context;
        }
    }
}
