namespace Geevers.Infrastructure.Test
{
    using System;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HttpStatusCodeExtensionsTest
    {
        [TestMethod]
        public void IsSuccessStatusCode_IsTrue_ForSuccessfullStatusCodes()
        {
            // arrange
            for (var i = 200; i < 300; i++)
            {
                var status = (HttpStatusCode)i;

                // act
                var result = status.IsSuccessStatusCode();

                // assert
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void IsSuccessStatusCode_IsFalse_ForUnsuccessfullStatusCodes()
        {
            // arrange
            for (var i = 0; i < 200; i++)
            {
                var status = (HttpStatusCode)i;

                // act
                var result = status.IsSuccessStatusCode();

                // assert
                Assert.IsFalse(result);
            }

            // arrange
            for (var i = 300; i < 1000; i++)
            {
                var status = (HttpStatusCode)i;

                // act
                var result = status.IsSuccessStatusCode();

                // assert
                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void IfStatus_Does_MatchCondition_IsReturnsTrueAndStatus()
        {
            // arrange
            var input = HttpStatusCode.BadGateway;

            // act
            var result = input.Is(HttpStatusCode.BadGateway, out var output);

            // assert
            Assert.IsTrue(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void IfStatus_DoesNot_MatchCondition_IsReturnsFalseAndStatus()
        {
            // arrange
            var input = HttpStatusCode.BadGateway;

            // act
            var result = input.Is(HttpStatusCode.BadRequest, out var output);

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void IfStatus_Does_MatchCondition_IsNotReturnsTrueAndStatus()
        {
            // arrange
            var input = HttpStatusCode.BadGateway;

            // act
            var result = input.IsNot(HttpStatusCode.BadGateway, out var output);

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void IfStatus_DoesNot_MatchCondition_IsNotReturnsFalseAndStatus()
        {
            // arrange
            var input = HttpStatusCode.BadGateway;

            // act
            var result = input.IsNot(HttpStatusCode.BadRequest, out var output);

            // assert
            Assert.IsTrue(result);
            Assert.AreEqual(input, output);
        }
    }
}
