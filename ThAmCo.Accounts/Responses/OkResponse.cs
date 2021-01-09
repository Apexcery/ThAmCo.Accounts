using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThAmCo.Accounts.Responses
{
    public class OkResponse : BaseResponse
    {
        public OkResponse() { }
        public OkResponse(string message) : base(message) { }
    }

    public class OkResponse<T> : BaseResponse<T>
    {
        public OkResponse() { }
        public OkResponse(string message) : base(message) { }
        public OkResponse(T value) : base(value) { }
        public OkResponse(string message, T value) : base(message, value) { }
    }
}
