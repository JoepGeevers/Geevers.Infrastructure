namespace Geevers.Infrastructure.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Dirty_IsDirty_Test
    {
        [TestMethod]
        public void ConstructorCreatedDirty_IsNotDirty()
        {
            // act
            var name = new Dirty<string>();

            // assert
            Assert.IsFalse(name.IsDirty);
        }

        [TestMethod]
        public void ImplicitlyCreatedDirty_IsNotDirty()
        {
            // act
            Dirty<string> name = default;

            // assert
            Assert.IsFalse(name.IsDirty);
        }

        [TestMethod]
        public void ConstructorAssignedDirty_IsDirty()
        {
            // act
            var name = new Dirty<string>("boop boop");

            // assert
            Assert.IsTrue(name.IsDirty);
        }

        [TestMethod]
        public void ImplicitlyAssignedDirty_IsDirty()
        {
            // act
            Dirty<string> name = "beep beep";

            // assert
            Assert.IsTrue(name.IsDirty);
        }

        [TestMethod]
        public void ConstructorCreatedDirty_AfterAssign_IsDirty()
        {
            // arrange
            var name = new Dirty<string>();

            // act
            name = "doo doo";

            // assert
            Assert.IsTrue(name.IsDirty);
        }

        [TestMethod]
        public void ImplicitlyCreatedDirty_AfterAssign_IsDirty()
        {
            // assert
            Dirty<string> name = default;

            // act
            name = "die die";

            // assert
            Assert.IsTrue(name.IsDirty);
        }
    }
}
