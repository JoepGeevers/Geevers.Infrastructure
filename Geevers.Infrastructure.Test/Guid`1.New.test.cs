namespace Geevers.Infrastructure.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Guid_1_New_Test
    {
        [TestMethod]
        public void NewGeneratesNonEmptyGuid()
        {
            // arrange
            var one = Guid<Guid_1_ImplicitOperator_Test>.New();

            // assert
            Assert.AreNotEqual(Guid.Empty, one.Value);
        }
    }
}
