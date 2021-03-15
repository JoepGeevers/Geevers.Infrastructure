namespace Geevers.Infrastructure.Test
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Response_2Tests
    {
        [TestMethod]
        public void DefaultResponse_HasStatusNotImplemented_AndDefaultResult()
        {
            // arrange
            Response<Point, Color> response = default;

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.NotImplemented, response.Status);
            Assert.AreEqual(default, response.Result);
            Assert.AreEqual(default, response.Error);
        }

        [TestMethod]
        public void WhenImplicitlyCastingTypeToResponse_ResponseStatusIsOkAndErrorIsNull()
        {
            // arrange
            // act
            Response<Point, Color> response = new Point(123);

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.Status);
            Assert.AreEqual(new Point(123), response.Result);
            Assert.AreEqual(default, response.Error);
        }

        [TestMethod]
        public void ImplicitlyCastingStatusOtherThenOkOrNoContent_ResponseGetsThatStatus()
        {
            // arrange
            var statii = Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Where(s => s != HttpStatusCode.OK)
                .Where(s => s != HttpStatusCode.NoContent);

            // act
            var responses = statii
                .Select(s => (Status: s, Response: (Response<Point, Color>)s));

            // assert
            Assert.IsTrue(responses.All(p => p.Response.Status == p.Status));
            Assert.IsTrue(responses.All(p => p.Response.Result == default));
            Assert.IsTrue(responses.All(p => p.Response.Error == default));
        }

        [TestMethod]
        public void ResponseCanBeCreatedAsTupleWithResult()
        {
            // arrange
            Response<Point, Color> response = (HttpStatusCode.Accepted, new Point(123));

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.Accepted, response.Status);
            Assert.AreEqual(new Point(123), response.Result);
            Assert.AreEqual(default, response.Error);
        }

        [TestMethod]
        public void ResponseCanBeCreatedAsTupleWithError()
        {
            // arrange
            Response<Point, Color> response = (HttpStatusCode.RequestEntityTooLarge, Color.RosyBrown);

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.RequestEntityTooLarge, response.Status);
            Assert.AreEqual(default, response.Result);
            Assert.AreEqual(Color.RosyBrown, response.Error);
        }

        [TestMethod]
        public void ImplicitlyCastingStatusNoContentResponse_ThrowsException()
        {
            // arrange
            Response<Point, Color> response = default;
            Exception exception = null;

            // act
            try
            {
                response = HttpStatusCode.NoContent;
            }
            catch (Exception e)
            {
                exception = e;
            }

            // assert
            Assert.AreEqual(default, response);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }
    }
}
