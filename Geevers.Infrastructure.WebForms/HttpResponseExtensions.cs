namespace Geevers.Infrastructure.WebForms
{
    using System.Net;
    using System.Threading;
    using System.Web;

    public static class HttpResponseExtensions
    {
        public static void EndWith(this HttpResponse response, HttpStatusCode status, bool swallowThreadAbortException = true)
        {
            response.Clear();
            response.StatusCode = (int)status;
            
            try
            {
                response.End();
            }
            catch (ThreadAbortException)
            {
                if (false == swallowThreadAbortException)
                {
                    throw;
                }
            }
        }
    }
}