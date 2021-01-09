using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThAmCo.Accounts.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public ErrorResponse()
        {
            IsError = true;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ErrorResponse(string message) : base(message)
        {
            IsError = true;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ErrorResponse(string message, int statusCode) : base(message)
        {
            IsError = true;
            StatusCode = statusCode;
        }
    }

    public class ErrorResponse<T> : BaseResponse<T>
    {
        public ErrorResponse()
        {
            IsError = true;
            StatusCode = StatusCodes.Status400BadRequest;
        }
        public ErrorResponse(string message) : base(message)
        {
            IsError = true;
            StatusCode = StatusCodes.Status400BadRequest;
        }
        public ErrorResponse(string message, int statusCode) : base(message)
        {
            IsError = true;
            StatusCode = statusCode;
        }
        public ErrorResponse(T value) : base(value)
        {
            IsError = true;
            StatusCode = StatusCodes.Status400BadRequest;
        }
        public ErrorResponse(string message, int statusCode, T value) : base(message, value)
        {
            IsError = true;
            StatusCode = statusCode;
        }
    }
}
