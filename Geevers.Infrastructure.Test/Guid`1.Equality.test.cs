namespace Geevers.Infrastructure.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Guid_1_Equality_Test
    {
        [TestMethod]
        public void IfFirstIsNullTheyAreNotTheSame()
        {
            // arrange
            var one = default(Guid<Id_2_Constructor_Test>);
            var two = Guid<Id_2_Constructor_Test>.New();

            // assert
            Assert.AreNotEqual(one, two);
            Assert.IsFalse(one == two);
            Assert.IsFalse(two == one);
            Assert.IsTrue(one != two);
            Assert.IsTrue(two != one);
        }

        [TestMethod]
        public void IfSecondIsNullTheyAreNotTheSame()
        {
            // arrange
            var two = default(Guid<Id_2_Constructor_Test>);
            var one = Guid<Id_2_Constructor_Test>.New();

            // assert
            Assert.AreNotEqual(one, two);
            Assert.IsFalse(one == two);
            Assert.IsFalse(two == one);
            Assert.IsTrue(one != two);
            Assert.IsTrue(two != one);
        }

        [TestMethod]
        public void IfBothAreNullTheyAreTheSame()
        {
            // arrange
            var one = default(Guid<Id_2_Constructor_Test>);
            var two = default(Guid<Id_2_Constructor_Test>);

            // assert
            Assert.IsTrue(one == two);
            Assert.IsFalse(one != two);
        }
    }
}
