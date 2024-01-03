using Monitor_BE.LogManage;

namespace Monitor_BE.ServerBusiness
{
    public class LogService : Loggers
    {
        public LogService()
        {
            new Loggers(@"C:\Users\kepe1\Documents\Leaen", "Study");
        }
    }
}
