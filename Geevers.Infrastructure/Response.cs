namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{Status}: {Result}")]
    public class Response<TResult>
    {
        public HttpStatusCode Status { get; internal set; }
        public TResult Result { get; private set; }
        public bool IsSuccessStatusCode
        {
            get
            {
                return this.Status.IsSuccessStatusCode();
            }
        }

        internal Response()
        {
        }

        public Response(TResult result)
        {
            this.Status = HttpStatusCode.OK;
            this.Result = result;
        }

        public Response(HttpStatusCode status)
        {
            if (status == HttpStatusCode.OK)
            {
                throw new InvalidOperationException("You may not construct a Response<TResult> with HttpStatusCode.OK. Please use HttpStatusCode.NoContent or an implicit result");
            }

            this.Status = status;
        }

        [Obsolete("Was used to trick Error results into Response. Will be removed when TError is introduced")]
        public Response(HttpStatusCode status, TResult result)
        {
            this.Status = status;
            this.Result = result;
        }

        public static implicit operator Response<TResult>(TResult result)
        {
            return new Response<TResult>(result);
        }

        public static implicit operator Response<TResult>(HttpStatusCode status)
        {
            return new Response<TResult>(status);
        }
    }
}