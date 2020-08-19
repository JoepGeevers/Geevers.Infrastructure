namespace Geevers.Infrastructure
{
    using System.Net;
    using System.Net.Http;

    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessStatusCode(this HttpStatusCode status)
        {
            return new HttpResponseMessage(status).IsSuccessStatusCode;
        }
    }
}