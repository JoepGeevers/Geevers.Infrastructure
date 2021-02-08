namespace Geevers.Infrastructure.Test
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResponseTests
    {
        [TestMethod]
        public void WhenImplicitlyCastingTypeToResponse_ResponseStatusIsOk()
        {
            // arrange
            var address = new MailAddress("anonymous@mailinator.com");

            // act
            Response<MailAddress> response = address;

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.Status);
            Assert.AreEqual(address, response.Result);
        }

        [TestMethod]
        public void ImplicitlyCastingStatusOkResponse_ThrowsException()
        {
            // arrange
            Response<Color> response = null;
            Exception exception = null;

            // act
            try
            {
                response = HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                exception = e;
            }

            // assert
            Assert.IsNull(response);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ImplicitlyCastingStatusOtherThenOkResponse_ResponseGetsThatStatus()
        {
            // arrange
            var statii = Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Where(s => s != HttpStatusCode.OK);

            // act
            var responses = statii
                .Select(s => (Status: s, Response: (Response<MailAddress>)s));

            // assert
            Assert.IsTrue(responses.All(p => p.Response.Status == p.Status));
            Assert.IsTrue(responses.All(p => p.Response.Result == null));
        }
    }
}