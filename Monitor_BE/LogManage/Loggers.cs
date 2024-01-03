using log4net;
using log4net.Appender;
using log4net.Config;
using System.Collections.Generic;

namespace Monitor_BE.LogManage
{
    public class Loggers : ILogs
    {
        private Dictionary<string, ILog> LogDic = new();
        private object _islock = new object();
        private string fileName = string.Empty;

        public Loggers() { }

        /// <summary>
        /// 日志调用初始化
        /// </summary>
        /// <param name="fileSavePath">日志文件保存路径[若路径为空，则默认程序根目录Logger文件夹;]</param>
        /// <param name="fileName">日志文件名[若文件名为空，则默认文件名：Default]</param>
        /// <param name="logSuffix"></param>
        public Loggers(string fileSavePath, string fileName, string logSuffix = ".log")
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(fileSavePath))
                    fileSavePath = "Logger";
                if (string.IsNullOrEmpty(fileName))
                    fileName = "Default";

                this.fileName = fileName;
                var repository = LogManager.GetRepository();
                var appenders = repository.GetAppenders();

                if (appenders.Length == 0) return;
                var targetApder = appenders.First(p => p.Name == "FileInfoAppender") as RollingFileAppender;
                targetApder.File = Path.Combine(fileSavePath, this.fileName + logSuffix);
                targetApder.ActivateOptions();
            }
            catch (Exception ex) { }
        }



        /// <summary>
        /// 缓存日志对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private ILog GetLog(string name)
        {
            try
            {
                if (LogDic == null)
                {
                    LogDic = new Dictionary<string, ILog>();
                }
                lock (_islock)
                {
                    if (!LogDic.ContainsKey(name))
                    {
                        LogDic.Add(name, LogManager.GetLogger(name));
                    }
                }
                return LogDic[name];
            }
            catch
            {
                return LogManager.GetLogger("Default");
            }
        }



        /// <summary>
        /// 日志记录初始化
        /// </summary>
        private void Init()
        {
            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4net.config"));
            XmlConfigurator.Configure(file);
        }

        /// <summary>
        /// 调试日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public void Debug(string msg)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Debug(msg);
        }



        /// <summary>
        /// 异常内容输出
        /// </summary>
        /// <param name="msg">输出</param>
        /// <param name="ex">异常</param>
        public void Debug(string msg, Exception ex)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Debug(msg, ex);
        }


        /// <summary>
        /// 信息输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public void Info(string msg)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Info(msg);
        }

        /// 

        /// 信息日志输出
        /// 

        /// 输出内容
        /// 输出异常
        public void Info(string msg, Exception ex)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Info(msg, ex);
        }

        /// 

        /// 警告日志输出
        /// 

        /// 输出内容
        public void Warn(string msg)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Warn(msg);
        }

        /// 

        /// 警告日志输出
        /// 

        /// 输出内容
        /// 输出异常
        public void Warn(string msg, Exception ex)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Warn(msg, ex);
        }

        /// 

        /// 错误日志输出
        /// 

        /// 输出内容
        public void Error(string msg)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Error(msg);
        }

        /// 

        /// 错误日志输出
        /// 

        /// 输出内容
        /// 输出异常
        public void Error(string msg, Exception ex)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Error(msg, ex);
        }

        /// 

        /// 致命日志输出
        /// 

        /// 输出内容
        public void Fatal(string msg)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Fatal(msg);
        }

        /// 

        /// 致命日志输出
        /// 

        /// 输出内容
        /// 输出异常
        public void Fatal(string msg, Exception ex)
        {
            var log = GetLog(this.fileName);
            if (log == null)
            {
                return;
            }
            log.Fatal(msg, ex);
        }
    }
}
