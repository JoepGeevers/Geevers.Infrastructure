namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Response<TResult, TError>
    {
        public HttpStatusCode Status => this.response.Status;
        public bool IsSuccessStatusCode => this.response.IsSuccessStatusCode;

        public TResult Result => this.response.Result;
        public TError Error { get; internal set; }

        private Response<TResult> response;

        private string DebuggerDisplay
        {
            get
            {
                return this.Status.IsSuccessStatusCode()
                    ? $"{this.Status}: {this.Result}"
                    : $"{this.Status}: {this.Error}";
            }
        }

        internal Response(Response<TResult> response)
        {
            this.response = response;
            this.Error = default;
        }

        internal Response(HttpStatusCode status, TError error)
        {
            if (status.IsSuccessStatusCode())
            {
                throw new InvalidOperationException("You cannot construct a Response with a successfull statuscode and an error");
            }

            this.response = status;
            this.Error = error;
        }

        public bool Is(HttpStatusCode cue, out TResult result, out TError error, out HttpStatusCode status)
        {
            result = this.Result;
            error = this.Error;
            status = this.Status;

            return status == cue;
        }

        public bool IsNot(HttpStatusCode cue, out TResult result, out TError error, out HttpStatusCode status)
        {
            return false == this.Is(cue, out result, out error, out status);
        }

        public static implicit operator Response<TResult, TError>(TResult result) => new Response<TResult, TError>(result);
        public static implicit operator Response<TResult, TError>(HttpStatusCode status) => new Response<TResult, TError>(status);
        public static implicit operator Response<TResult, TError>((HttpStatusCode status, TResult result) response) => new Response<TResult, TError>((response.status, response.result));
        public static implicit operator Response<TResult, TError>((HttpStatusCode status, TError error) response) => new Response<TResult, TError>(response.status, response.error);
    }
}