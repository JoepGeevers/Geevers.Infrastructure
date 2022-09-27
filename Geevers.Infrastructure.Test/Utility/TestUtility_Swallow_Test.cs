namespace Geevers.Infrastructure.Test.Utility
{
    using System;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Geevers.Infrastructure.Utility;

    [TestClass]
    public class TestUtility_Swallow_Test
    {
        [TestMethod]
        public void IfMethodIsNull_ReturnsException()
        {
            // act
            var response = TestUtility.Swallow<Func<int>>(default);

            // assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.IsInstanceOfType(response.Error, typeof(ArgumentNullException));
        }

        [TestMethod]
        public void IfMethodBlowsUp_ReturnsException()
        {
            // act
            var response = TestUtility.Swallow<Func<int>>(() => throw new NotImplementedException());

            // assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.IsInstanceOfType(response.Error, typeof(NotImplementedException));
        }

        [TestMethod]
        public void IfMethodIsFine_ReturnsResult()
        {
            // act
            var response = TestUtility.Swallow(() => 123);

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.Status);
            Assert.AreEqual(123, response.Result);
        }
    }
}
