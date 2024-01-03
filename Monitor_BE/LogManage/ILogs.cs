namespace Monitor_BE.LogManage
{
    public interface ILogs
    {

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="msg"></param>
        void Debug(string msg);

        /// <summary>
        /// 输出异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Debug(string msg, Exception ex);

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="msg"></param>
        void Info(string msg);

        /// <summary>
        /// 输出异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Info(string msg, Exception ex);

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);

        /// <summary>
        /// 输出异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Warn(string msg, Exception ex);

        /// <summary>
        /// 错误日志输出
        /// </summary>
        /// <param name="msg"></param>
        void Error(string msg);

        /// <summary>
        /// 错误日志输出
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Error(string msg, Exception ex);

        /// <summary>
        /// 致命日志输出
        /// </summary>
        /// <param name="msg"></param>
        void Fatal(string msg);

        /// <summary>
        /// 致命日志输出
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Fatal(string msg, Exception ex);

    }
}
