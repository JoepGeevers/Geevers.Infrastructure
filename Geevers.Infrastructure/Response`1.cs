namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{Status}: {Result}")]
    public struct Response<TResult>
    {
        public HttpStatusCode Status => this.status ?? HttpStatusCode.NotImplemented;
        public TResult Result { get; internal set; }

        private HttpStatusCode? status;

        public bool IsSuccessStatusCode => this.Status.IsSuccessStatusCode();

        internal Response(TResult result)
        {
            this.status = HttpStatusCode.OK;
            this.Result = result;
        }

        internal Response(HttpStatusCode status)
        {
            this.status = status;
            this.Result = default;

            if (status == HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"You cannot create a Response from statuscode OK because it múst have a result. Either return a result or use HttpStatusCode as a return type and NoContent as a value");
            }

            if (status == HttpStatusCode.NoContent)
            {
                throw new InvalidOperationException("You cannot create a Response with statuscode NoContent because Response is supposed to have a result. Either return a result or use HttpStatusCode as a return type and NoContent as a value");
            }
        }

        internal Response(HttpStatusCode status, TResult result)
        {
            this.status = status;
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

        public static implicit operator Response<TResult>(TResult result) => new Response<TResult>(result);
        public static implicit operator Response<TResult>(HttpStatusCode status) => new Response<TResult>(status);
        public static implicit operator Response<TResult>((HttpStatusCode status, TResult result) response) => new Response<TResult>(response.status, response.result);
    }
}