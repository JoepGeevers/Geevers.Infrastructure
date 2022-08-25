namespace Geevers.Infrastructure.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Id_2_ImplicitOperator_Test
    {
        [TestMethod]
        public void IdIsEqualToValueWithImplicitOperator()
        {
            // arrange
            var one = new Id<object, int>(123);

            // assert
            Assert.AreEqual(one, 123);
            Assert.IsTrue(one.Equals(123));
            Assert.IsTrue(one == 123);
            Assert.IsFalse(one != 123);
        }

        [TestMethod]
        public void IdIsNotEqualToValueWithImplicitOperator()
        {
            // arrange
            var one = new Id<object, int>(123);

            // assert
            Assert.AreNotEqual(one, 124);
            Assert.IsFalse(one.Equals(124));
            Assert.IsFalse(one == 124);
            Assert.IsTrue(one != 124);
        }
    }
}
