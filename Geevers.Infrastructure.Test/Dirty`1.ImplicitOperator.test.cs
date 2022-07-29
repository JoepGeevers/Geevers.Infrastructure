namespace Geevers.Infrastructure.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Dirty_Implicit_Test
    {
        [TestMethod]
        public void Foo()
        {
            // arrange
            var name = new Dirty<string>();

            // act
            name = "plastering heights";

            // assert
            Assert.IsTrue("plastering heights" == name);
            Assert.IsTrue(name == "plastering heights");
        }
    }
}
