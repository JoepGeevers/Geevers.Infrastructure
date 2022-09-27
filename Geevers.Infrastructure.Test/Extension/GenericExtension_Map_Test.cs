namespace Geevers.Infrastructure.Test.Extension
{
    using System;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Geevers.Infrastructure.Extension;
    using Geevers.Infrastructure.Utility;

    [TestClass]
    public class GenericExtension_Map_Test
    {
        [TestMethod]
        public void IfMapperMethodIsNull_ReturnsException()
        {
            // act
            var response = TestUtility.Swallow(() => 123.Map<int, int>(default));

            // assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.IsInstanceOfType(response.Error, typeof(ArgumentNullException));
        }

        [TestMethod]
        public void IfMapperSourceMethodIsNull_ReturnsException()
        {
            // act
            var response = TestUtility.Swallow(() => default(int?).Map<int?, int?>(default));

            // assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.IsInstanceOfType(response.Error, typeof(ArgumentNullException));
        }

        [TestMethod]
        public void IfMapperSourceAndSelectorAreFine_ReturnsValue()
        {
            // act
            var result = 123.Map(i => i * 2);

            // assert
            Assert.AreEqual(246, result);
        }
    }
}