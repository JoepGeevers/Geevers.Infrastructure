namespace Geevers.Infrastructure.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Guid_1_ImplicitOperator_Test
    {
        [TestMethod]
        public void IdIsEqualToValueWithImplicitOperator()
        {
            // arrange
            var guid = Guid.NewGuid();

            var one = new Guid<Guid_1_ImplicitOperator_Test>(guid);

            // assert
            Assert.AreEqual(guid, one);
            Assert.IsTrue(one.Equals(guid));
            Assert.IsTrue(one == guid);
            Assert.IsFalse(one != guid);
        }

        [TestMethod]
        public void IdIsNotEqualToValueWithImplicitOperator()
        {
            // arrange
            var one = new Guid<Guid_1_ImplicitOperator_Test>(Guid.NewGuid());

            // assert
            Assert.AreNotEqual(Guid.NewGuid(), one);
            Assert.IsFalse(one.Equals(Guid.NewGuid()));
            Assert.IsFalse(one == Guid.NewGuid());
            Assert.IsTrue(one != Guid.NewGuid());
        }
    }
}
