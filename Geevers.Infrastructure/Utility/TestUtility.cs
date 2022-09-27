namespace Geevers.Infrastructure.Utility
{
    using System;
    using System.Net;

    public class TestUtility
    {
        public static Response<T, Exception> Swallow<T>(Func<T> method)
        {
            try
            {
                if (method == null)
                {
                    throw new ArgumentNullException(nameof(method));
                }

                return method();
            }
            catch (Exception e)
            {
                return (HttpStatusCode.InternalServerError, e);
            }
        }
    }
}