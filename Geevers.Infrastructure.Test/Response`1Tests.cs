namespace Geevers.Infrastructure.Test
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Response_1_Tests
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
            Response<Color> response = default;
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
            Assert.AreEqual(default, response);
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
            Response<int> response = (HttpStatusCode.Created, 42);

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
            Response<int> response = (HttpStatusCode.Created, 42);

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
            Response<int> response = (HttpStatusCode.Created, 42);

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
            Response<int> response = (HttpStatusCode.Created, 42);

            // act
            var IsResult = response.IsNot(HttpStatusCode.Created, out var result, out var status);

            // assert
            Assert.IsFalse(IsResult);
            Assert.AreEqual(response.Status, status);
            Assert.AreEqual(response.Result, result);
        }

        [TestMethod]
        public void DefaultResponse_HasStatusNotImplemented_AndDefaultResult()
        {
            // arrange
            Response<Color> response = default;

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.NotImplemented, response.Status);
            Assert.AreEqual(default, response.Result);
        }

        [TestMethod]
        public void ResponseCanBeCreatedAsTuple()
        {
            // arrange
            Response<Color> response = (HttpStatusCode.RequestEntityTooLarge, Color.Blue);

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.RequestEntityTooLarge, response.Status);
            Assert.AreEqual(Color.Blue, response.Result);
        }

        [TestMethod]
        public void WhenTResultIsAnInterface_WeNeedAccessToResponseConstructorToCreateAResponse()
        {
            // arrange
            ICollection<Color> colors = new List<Color>();

            // act
            Response<ICollection<Color>> response = new Response<ICollection<Color>>(colors); // Compiler does not allow `Response<ICollection<Color>> response = colors`

            // assert
            Assert.AreEqual(colors, response.Result);
        }
    }
}