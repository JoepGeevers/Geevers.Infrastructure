namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{Status}: {Result}")]
    public class Response<TResult>
    {
        public HttpStatusCode Status { get; internal set; }
        public TResult Result { get; internal set; }

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
                throw new InvalidOperationException("You should not need to construct a Response<TResult> with HttpStatusCode.OK. Did you mean NoContent? IF not, please either just return the result, or a `new OK(T)`");
            }

            this.Status = status;
        }

        [Obsolete("Was used to trick Error results into Response. Will be removed when TError is introduced")]
        public Response(HttpStatusCode status, TResult result)
        {
            this.Status = status;
            this.Result = result;
        }

        public bool Is(HttpStatusCode cue, out TResult result, out HttpStatusCode status)
        {
            result = this.Result;
            status = this.Status;

            return status == cue;
        }

        public bool IsNot(HttpStatusCode cue, out TResult result, out HttpStatusCode status)
        {
            return false == this.Is(cue, out result, out status);
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