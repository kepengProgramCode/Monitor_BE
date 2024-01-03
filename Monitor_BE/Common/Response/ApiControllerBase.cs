using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Monitor_BE.Common.Api;
using Monitor_BE.Common.Token;
using NewLife;
using NewLife.Log;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Serialization;
using NewLife.Web;
using System.Reflection;

namespace Monitor_BE.Common.Response
{
    //[Route("[controller]/[Action]")]
    //[ApiController]
    /// <summary>业务接口控制器基类</summary>
    /// <remarks>
    /// 提供统一的令牌解码验证架构
    /// </remarks>
    [ApiFilter]
    public abstract class ApiControllerBase : ControllerBase, IActionFilter
    {
        protected ResponseResult<T> SuccessResult<T>(T result)
        {
            return ResponseResult<T>.SuccessResult(result);
        }

        protected ResponseResult<T> FailResult<T>(string? msg)
        {
            return ResponseResult<T>.FailResult(msg);
        }

        protected ResponseResult<T> ErrorResult<T>(string? msg)
        {
            return ResponseResult<T>.ErrorResult(msg);
        }

        protected ResponseResult<T> Result<T>(ResultStatus status, T data, string? msg)
        {
            return ResponseResult<T>.Result(status, data, msg);
        }


        #region 属性
        /// <summary>令牌</summary>
        public string? Token { get; private set; }

        /// <summary>用户主机</summary>
        //public string UserHost => HttpContext.GetUserHost();

        private IDictionary<string, object>? _args;
        #endregion

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) WriteError(context.Exception, context);
        }

        #region 令牌验证
        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            _args = context.ActionArguments;

            var token = Token = ApiFilterAttribute.GetToken(context.HttpContext);

            try
            {
                var rs = !token.IsNullOrEmpty() && OnAuthorize(token);
                if (!rs && context.ActionDescriptor is ControllerActionDescriptor act && !act.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute)))
                    throw new ApiException(403, "认证失败");
            }
            catch (Exception ex)
            {
                var traceId = DefaultSpan.Current?.TraceId;
                context.Result = ex is ApiException aex
                    ? new JsonResult(new { code = aex.Code, data = aex.Message, traceId })
                    : new JsonResult(new { code = 500, data = ex.Message, traceId });

                WriteError(ex, context);
            }
        }

        private void WriteError(Exception ex, ActionContext context)
        {
            // 拦截全局异常，写日志
            var action = context.HttpContext.Request.Path + "";
            if (context.ActionDescriptor is ControllerActionDescriptor act) action = $"{act.ControllerName}/{act.ActionName}";
            OnWriteError(action, ex?.GetTrue() + Environment.NewLine + _args?.ToJson(true));
        }

        protected abstract void OnWriteError(string action, string message);
        protected bool OnAuthorize(string token)
        {
            return TokenAuth.DecodeToken(token, TokenAuth.jwtConfig.TokenSecret);
        }
        #endregion

    }
}
