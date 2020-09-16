namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{Status}: {Result}")]
    public class Response<T>
    {
        public HttpStatusCode Status { get; private set; }
        public T Result { get; private set; }
        public bool IsSuccessStatusCode
        {
            get
            {
                return this.Status.IsSuccessStatusCode();
            }
        }

        private Response()
        {
        }

        public Response(T result)
        {
            this.Status = HttpStatusCode.OK;
            this.Result = result;
        }

        public Response(HttpStatusCode status)
        {
            this.Status = status;
        }

        [Obsolete("a future improvement will allow using higher resolution status codes")]
        public Response(HttpStatusCode status, T result)
        {
            this.Status = status;
            this.Result = result;
        }

        public static implicit operator Response<T>(T result)
        {
            return new Response<T>(result);
        }

        public static implicit operator Response<T>(HttpStatusCode status)
        {
            return new Response<T>(status);
        }
    }
}