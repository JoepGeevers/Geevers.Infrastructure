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

        public static bool Is(this HttpStatusCode status, HttpStatusCode cue, out HttpStatusCode outStatus)
        {
            outStatus = status;

            return cue == status;
        }

        public static bool IsNot(this HttpStatusCode status, HttpStatusCode cue, out HttpStatusCode outStatus)
        {
            return false == status.Is(cue, out outStatus);
        }
    }
}