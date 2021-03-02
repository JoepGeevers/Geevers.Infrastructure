namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Response<TResult, TError>
    {
        private HttpStatusCode? status;

        public HttpStatusCode Status
        {
            get
            {
                return this.status ?? HttpStatusCode.NotImplemented;
            }
            internal set
            {
                this.status = value;
            }
        }

        private string DebuggerDisplay
        {
            get
            {
                return this.Status.IsSuccessStatusCode()
                    ? $"{this.Status}: {this.Result}"
                    : $"{this.Status}: {this.Error}";
            }
        }

        public TResult Result { get; internal set; }
        public TError Error { get; internal set; }

        public bool IsSuccessStatusCode
        {
            get
            {
                return this.Status.IsSuccessStatusCode();
            }
        }

        public Response(TResult result)
        {
            this.status = HttpStatusCode.OK;
            this.Result = result;
            this.Error = default;
        }

        public Response(HttpStatusCode status)
        {
            if (status == HttpStatusCode.OK)
            {
                throw new InvalidOperationException("You should not need to construct a Response<TResult> with HttpStatusCode.OK. Did you mean NoContent? If not, please either just return the result, or a `new OK(T)`");
            }

            this.status = status;
            this.Result = default;
            this.Error = default;
        }

        internal Response(HttpStatusCode status, TResult result)
        {
            if (false == status.IsSuccessStatusCode())
            {
                throw new InvalidOperationException("You cannot construct a Response with an unsuccessfull statuscode and a result");
            }

            this.status = status;
            this.Result = result;
            this.Error = default;
        }

        internal Response(HttpStatusCode status, TError error)
        {
            if (status.IsSuccessStatusCode())
            {
                throw new InvalidOperationException("You cannot construct a Response with a successfull statuscode and an error");
            }

            this.status = status;
            this.Result = default;
            this.Error = error;
        }

        public static implicit operator Response<TResult, TError>(TResult result)
        {
            return new Response<TResult, TError>(result);
        }

        public static implicit operator Response<TResult, TError>(HttpStatusCode status)
        {
            return new Response<TResult, TError>(status);
        }

        public static implicit operator Response<TResult, TError>((HttpStatusCode status, TResult result) response)
        {
            return new Response<TResult, TError>(response.status, response.result);
        }

        public static implicit operator Response<TResult, TError>((HttpStatusCode status, TError error) response)
        {
            return new Response<TResult, TError>(response.status, response.error);
        }
    }
}