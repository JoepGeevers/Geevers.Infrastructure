namespace Geevers.Infrastructure.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Id_2_ToString_Test
    {
        [TestMethod]
        public void ToStringJustCallsToStringOnBoxedValue()
        {
            // arrange
            var id = new Id<Id_2_Constructor_Test, Hello>(new Hello());

            // act
            var actual = id.ToString();

            // assert
            Assert.AreEqual("hi!", actual);
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