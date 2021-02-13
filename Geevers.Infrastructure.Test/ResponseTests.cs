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

        [TestMethod]
        public void IfResponseStatus_DoesNot_MatchCondition_Is_ReturnsFalseAndOutVarsAreSet()
        {
            // arrange
            Response<int> response = new Created<int>(42);

            var statii = Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Where(s => s != HttpStatusCode.Created);

            // act
            var responses = statii
                .Select(s => {
                    var a = response.Is(s, out var result, out var status);

                    return (InStatus: s, IsResult: a, OutResult: result, OutStatus: s);
                });

            // assert
            Assert.IsTrue(responses.All(p => p.InStatus == p.OutStatus));
            Assert.IsTrue(responses.All(p => false == p.IsResult));
            Assert.IsTrue(responses.All(p => p.OutResult == 42));
        }

        [TestMethod]
        public void IfResponseStatus_Does_MatchCondition_Is_ReturnsTrueAndOutVarsAreSet()
        {
            // arrange
            Response<int> response = new Created<int>(42);

            // act
            var IsResult = response.Is(HttpStatusCode.Created, out var result, out var status);

            // assert
            Assert.AreEqual(response.Status, status);
            Assert.AreEqual(response.Result, result);
            Assert.IsTrue(IsResult);
        }

        [TestMethod]
        public void IfResponseStatus_DoesNot_MatchCondition_NotIs_ReturnsTrueAndOutVarsAreSet()
        {
            // arrange
            Response<int> response = new Created<int>(42);

            var statii = Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Where(s => s != HttpStatusCode.Created);

            // act
            var responses = statii
                .Select(s => {
                    var a = response.IsNot(s, out var result, out var status);

                    return (InStatus: s, IsResult: a, OutResult: result, OutStatus: s);
                });

            // assert
            Assert.IsTrue(responses.All(p => p.IsResult));
            Assert.IsTrue(responses.All(p => p.InStatus == p.OutStatus));
            Assert.IsTrue(responses.All(p => p.OutResult == 42));
        }

        [TestMethod]
        public void IfResponseStatus_Does_MatchCondition_IsNot_ReturnsFalseAndOutVarsAreSet()
        {
            // arrange
            Response<int> response = new Created<int>(42);

            // act
            var IsResult = response.IsNot(HttpStatusCode.Created, out var result, out var status);

            // assert
            Assert.IsFalse(IsResult);
            Assert.AreEqual(response.Status, status);
            Assert.AreEqual(response.Result, result);
        }

        [TestMethod]
        public void OK_IsIdenticalToOkResponse()
        {
            // arrange
            var response = new OK<Color>(Color.BlanchedAlmond);

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.Status);
            Assert.AreEqual(Color.BlanchedAlmond, response.Result);
        }

        [TestMethod]
        public void Created_IsIdenticalToResponseCreatedWithResult()
        {
            // arrange
            var response = new Created<Color>(Color.BlanchedAlmond);

            // assert
            Assert.AreEqual(HttpStatusCode.Created, response.Status);
            Assert.AreEqual(Color.BlanchedAlmond, response.Result);
        }
    }
}