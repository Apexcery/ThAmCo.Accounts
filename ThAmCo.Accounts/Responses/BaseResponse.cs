
using Microsoft.AspNetCore.Http;

namespace ThAmCo.Accounts.Responses
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public bool IsError { get; set; } = false;
        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public BaseResponse() { }

        public BaseResponse(string message)
        {
            Message = message;
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse() : base() { }
        public BaseResponse(string message) : base(message) { }
        public BaseResponse(T value)
        {
            Value = value;
        }
        public BaseResponse(string message, T value) : base(message)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
