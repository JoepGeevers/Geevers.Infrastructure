namespace Geevers.Infrastructure
{
    using System.Net;

    public class Created<TResult> : Response<TResult>
    {
        public Created(TResult value) : base(HttpStatusCode.Created, value) { }
    }
}