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
            Assert.AreEqual(address, response.Value);
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
            Assert.IsTrue(responses.All(p => p.Response.Value == null));
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
            Assert.AreEqual(response.Value, result);
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
            Assert.AreEqual(response.Value, result);
        }

        [TestMethod]
        public void DefaultResponse_HasStatusNotImplemented_AndDefaultResult()
        {
            // arrange
            Response<Color> response = default;

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.NotImplemented, response.Status);
            Assert.AreEqual(default, response.Value);
        }

        [TestMethod]
        public void ResponseCanBeCreatedAsTuple()
        {
            // arrange
            Response<Color> response = (HttpStatusCode.Accepted, Color.Blue);

            // act
            // assert
            Assert.AreEqual(HttpStatusCode.Accepted, response.Status);
            Assert.AreEqual(Color.Blue, response.Value);
        }

        [TestMethod]
        public void CreatingResultFromInterfaceCanBeDoneWithTheTupleOperator()
        {
            // arrange
            ICollection<Color> colors = new List<Color>();

            // act
            Response<ICollection<Color>> response = (HttpStatusCode.OK, colors); // Compiler does not allow `Response<ICollection<Color>> response = colors`

            // assert
            Assert.AreEqual(colors, response.Value);
        }

        [TestMethod]
        public void EveryResponseCreatedWithAnyStatusAndResult_PropertyValueHoldsValue()
        {
            // arrange
            Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .ToList()
                .ForEach(EveryResponseCreatedWithStatusAndResult_PropertyValueHoldsValue);
        }

        private void EveryResponseCreatedWithStatusAndResult_PropertyValueHoldsValue(HttpStatusCode s)
        {
            // act
            var response = (Response<int>)(s, 42);

            // assert
            Assert.AreEqual(s, response.Status);
            Assert.AreEqual(42, response.Value);
        }
    }
}