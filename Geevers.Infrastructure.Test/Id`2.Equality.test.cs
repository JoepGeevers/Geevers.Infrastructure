namespace Geevers.Infrastructure.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Id_2_Equality_Test
    {
        [TestMethod]
        public void DifferentEntitiesWithEqualKeysAreNotTheSame()
        {
            // arrange
            var one = new Id<object, int>(123);
            var two = new Id<Id_2_Constructor_Test, int>(123);

            // assert
            Assert.AreNotEqual(one, two);
            Assert.IsFalse(one.Equals(two));
        }

        [TestMethod]
        public void EqualEntitiesWithDifferentKeysAreNotTheSame()
        {
            // arrange
            var one = new Id<Id_2_Constructor_Test, int>(123);
            var two = new Id<Id_2_Constructor_Test, int>(456);

            // assert
            Assert.AreNotEqual(one, two);
            Assert.IsFalse(one.Equals(two));
            Assert.IsFalse(one == two);
            Assert.IsTrue(one != two);
        }

        [TestMethod]
        public void EqualEntitiesWithEqualKeysAreTheSame()
        {
            // arrange
            var one = new Id<Id_2_Constructor_Test, int>(123);
            var two = new Id<Id_2_Constructor_Test, int>(123);

            // assert
            Assert.AreEqual(one, two);
            Assert.IsTrue(one.Equals(two));
            Assert.IsTrue(one == two);
            Assert.IsFalse(one != two);
        }

        [TestMethod]
        public void IfFirstIsNullTheyAreNotTheSame()
        {
            // arrange
            var one = default(Id<Id_2_Constructor_Test, int>);
            var two = new Id<Id_2_Constructor_Test, int>(123);

            // assert
            Assert.AreNotEqual(one, two);
            Assert.IsFalse(one == two);
            Assert.IsTrue(one != two);
        }

        [TestMethod]
        public void IfSecondIsNullTheyAreNotTheSame()
        {
            // arrange
            var one = new Id<Id_2_Constructor_Test, int>(123);
            var two = default(Id<Id_2_Constructor_Test, int>);

            // assert
            Assert.AreNotEqual(one, two);
            Assert.IsFalse(one == two);
            Assert.IsTrue(one != two);
        }

        [TestMethod]
        public void IfBothAreNullTheyAreNotTheSame()
        {
            // arrange
            var one = default(Id<Id_2_Constructor_Test, int>);
            var two = default(Id<Id_2_Constructor_Test, int>);

            // assert
            Assert.IsFalse(one == two);
            Assert.IsTrue(one != two);
        }
    }
}