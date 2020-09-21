namespace Geevers.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;

    [DebuggerDisplay("{Status}: {Result}")]
    public class Response<TResult> : Response<HttpStatusCode, TResult>
    {
        public HttpStatusCode Status { get; private set; }

        private Response()
        {
        }

        public Response(TResult result)
        {
            this.Status = HttpStatusCode.OK;
            this.Result = result;
        }

        public Response(HttpStatusCode status)
        {
            this.Status = status;
        }

        [Obsolete("Use Response<TStatusCode, TResult> for custom statuscodes")]
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

    [DebuggerDisplay("{StatusCode}: {Result}")]
    public class Response<TStatusCode, TResult>
        where TStatusCode : Enum
    {
        private const int magic = int.MaxValue;

        public TStatusCode StatusCode { get; set; }
        public TResult Result { get; internal set; }
        public HttpStatusCode HttpStatusCode
        {
            get
            {
                if ((int)(object)this.StatusCode == magic)
                {
                    return HttpStatusCode.OK;
                }

                return Map[typeof(TStatusCode)][this.StatusCode];
            }
        }

        public bool IsSuccessStatusCode => this.HttpStatusCode.IsSuccessStatusCode();

        internal Response()
        {
        }

        public Response(TResult result)
        {
            this.StatusCode = (TStatusCode)(object)magic;
            this.Result = result;
        }

        public Response(TStatusCode status)
        {
            this.StatusCode = status;
        }

        public static implicit operator Response<TStatusCode, TResult>(TResult result) => new Response<TStatusCode, TResult>(result);
        public static implicit operator Response<TStatusCode, TResult>(TStatusCode status) => new Response<TStatusCode, TResult>(status);

        static Response() => ValidateStatusCodeType();

        private static Dictionary<Type, Dictionary<TStatusCode, HttpStatusCode>> Map = new Dictionary<Type, Dictionary<TStatusCode, HttpStatusCode>>();

        private static void ValidateStatusCodeType()
        {
            var TStatusCodeType = typeof(TStatusCode);

            if (Map.ContainsKey(TStatusCodeType))
            {
                return;
            }

            var HttpStatusCodeType = typeof(HttpStatusCode);

            if (TStatusCodeType == HttpStatusCodeType)
            {
                return;
            }

            Map[TStatusCodeType] = Enum.GetValues(TStatusCodeType)
                .Cast<TStatusCode>()
                .Select(s => new {
                    StatusCode = s,
                    HttpStatusCode = Find(s),
                })
                .ToDictionary(a => a.StatusCode, b => b.HttpStatusCode);
        }

        private static HttpStatusCode Find(TStatusCode s)
        {
            var longest = Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Where(c => s.ToString().EndsWith(c.ToString()))
                .OrderByDescending(c => c.ToString().Length)
                .FirstOrDefault();

            if (longest == default)
            {
                throw new NotSupportedException($"Value `{s}` of Enum `{typeof(TStatusCode).Name}` cannot be mapped to HttpStatusCode");
            }

            return longest;
        }
    }
}