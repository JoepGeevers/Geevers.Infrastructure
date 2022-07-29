namespace Geevers.Infrastructure.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Dirty_IfDirty_Test
    {
        [TestMethod]
        public void IfDirty_ActionIsExecutedWithValue()
        {
            // arrange
            var name = new Dirty<string>("bye bye vettel");

            var result = default(string);

            // act
            name.IfDirty(value => result = value);

            // assert
            Assert.AreEqual("bye bye vettel", result);
        }

        [TestMethod]
        public void IfNotDirty_ActionIsNotExecuted()
        {
            // arrange
            Dirty<string> name = default;

            // act & assert
            name.IfDirty(value => throw new Exception());
        }
    }
}
