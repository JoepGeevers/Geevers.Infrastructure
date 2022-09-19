namespace Geevers.Infrastructure.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Guid_1_ToString_Test
    {
        [TestMethod]
        public void ToStringJustCallsToStringOnBoxedValue()
        {
            // arrange
            var guid = Guid.NewGuid();
            var id = new Guid<Guid_1_ToString_Test>(guid);

            // act
            var actual = id.ToString();

            // assert
            Assert.AreEqual(guid.ToString(), actual);
        }

        public class Hello
        {
            public override string ToString()
            {
                return "hi!";
            }
        }
    }
}
