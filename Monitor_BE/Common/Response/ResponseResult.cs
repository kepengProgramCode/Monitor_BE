using Monitor_BE.Entity;
using Newtonsoft.Json.Linq;

namespace Monitor_BE.Common.Response
{
    public class ResponseResult<T>
    {
        public ResultStatus Code { get; set; } = ResultStatus.Success;
        public string? Message { get => !string.IsNullOrEmpty(message) ? message : Fun.GetEnumDescription(Code); set => message = value; }

        private string? message;

        public T? Data { get; set; }

        public static ResponseResult<T> SuccessResult(T data)
        {
            return new ResponseResult<T> { Data = data, Code = ResultStatus.Success };
        }

        public static ResponseResult<T> FailResult(string? message = null)
        {
            return new ResponseResult<T> { Message = message, Code = ResultStatus.Fail };
        }

        public static ResponseResult<T> ErrorResult(string? message = null)
        {
            return new ResponseResult<T> { Message = message, Code = ResultStatus.Error };
        }

        public static ResponseResult<T> Result(ResultStatus status, T data, string? message = null)
        {
            return new ResponseResult<T> { Message = message, Code = status, Data = data };
        }

        /// <summary>
        /// 将T 隐士转换成ResponseResult<T>
        /// </summary>
        /// <param name="data"></param>
        public static implicit operator ResponseResult<T>(T data)
        {
            return new ResponseResult<T> { Data = data };
        }
    }
}
