namespace Geevers.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Net;

    [DebuggerDisplay("{Status}: {Result}")]
    public struct Response<TValue>
    {
        private HttpStatusCode? status;
        private TValue value;

        public HttpStatusCode Status => this.status ?? HttpStatusCode.NotImplemented;
        public bool IsSuccessStatusCode => this.Status.IsSuccessStatusCode();
        public TValue Result => this.IsSuccessStatusCode ? this.value : default;
        public TValue Error => this.IsSuccessStatusCode ? default : this.value;

        [Obsolete("Use `Result` or `Error` to obtain value. `Value` will be removed in a future version")]
        public TValue Value => this.value;

        internal Response(TValue value)
        {
            this.status = HttpStatusCode.OK;
            this.value = value;
        }

        internal Response(HttpStatusCode status)
        {
            if (status == HttpStatusCode.OK)
            {
                throw new InvalidOperationException("You should not need to construct a Response<TResult> with HttpStatusCode.OK. Did you mean NoContent? If not, please either just return the result, or a `new OK(T)`");
            }

            this.status = status;
            this.value = default;
        }

        internal Response(HttpStatusCode status, TValue value)
        {
            this.status = status;
            this.value = value;
        }

        public bool Is(HttpStatusCode cue, out TValue value, out HttpStatusCode status)
        {
            status = this.Status;
            value = this.value;

            return status == cue;
        }

        public bool IsNot(HttpStatusCode cue, out TValue value, out HttpStatusCode status)
        {
            return false == this.Is(cue, out value, out status);
        }

        public static implicit operator Response<TValue>(TValue value) => new Response<TValue>(value);
        public static implicit operator Response<TValue>(HttpStatusCode status) => new Response<TValue>(status);
        public static implicit operator Response<TValue>((HttpStatusCode status, TValue value) response) => new Response<TValue>(response.status, response.value);
    }
}