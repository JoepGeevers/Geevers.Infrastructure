namespace Geevers.Infrastructure
{
    using System.Net;

    public class OK<TResult> : Response<TResult>
    {
        public OK(TResult value) : base(HttpStatusCode.OK, value) { }
    }
}