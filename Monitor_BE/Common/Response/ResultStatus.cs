using System.ComponentModel;

namespace Monitor_BE.Common.Response
{
    public enum ResultStatus
    {
        [Description("请求成功")]
        Success = 0,
        [Description("请求失败")]
        Fail = 1,
        [Description("请求异常")]
        Error = -1,
    }
}
