namespace Geevers.Infrastructure.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Id_2_Constructor_Test
    {
        [TestMethod]
        public void BoxedValueIsRemembered()
        {
            // act
            var id = new Id<Id_2_Constructor_Test, int>(123);

            // assert
            Assert.AreEqual(123, id.Value);
        }

        [TestMethod]
        public void DefaultKeyIntIsNotAllowed()
        {
            // arrange
            var caught = default(ArgumentException);

            // act
            try
            {
                var id = new Id<Id_2_Constructor_Test, int>(0);
            }
            catch (ArgumentException e)
            {
                caught = e;
            }

            // assert
            Assert.IsNotNull(caught);
            Assert.AreEqual("Id<T> cannot be null, default or empty", caught.Message);
        }

        [TestMethod]
        public void DefaultKeyGuidIsNotAllowed()
        {
            // arrange
            var caught = default(ArgumentException);

            // act
            try
            {
                var id = new Id<Id_2_Constructor_Test, Guid>(Guid.Empty);
            }
            catch (ArgumentException e)
            {
                caught = e;                                                                                                                                                                                                                 
            }

            // assert
            Assert.IsNotNull(caught);
            Assert.AreEqual("Id<T> cannot be null, default or empty", caught.Message);
        }

        [TestMethod]
        public void DefaultKeyStringIsNotAllowed()
        {
            // arrange
            var caught = default(ArgumentException);

            // act
            try
            {
                var id = new Id<Id_2_Constructor_Test, string>(null);
            }
            catch (ArgumentException e)
            {
                caught = e;
            }

            // assert
            Assert.IsNotNull(caught);
            Assert.AreEqual("Id<T> cannot be null, default or empty", caught.Message);
        }

        [TestMethod]
        public void EmptyKeyStringIsNotAllowed()
        {
            // arrange
            var caught = default(ArgumentException);

            // act
            try
            {
                var id = new Id<Id_2_Constructor_Test, string>("");
            }
            catch (ArgumentException e)
            {
                caught = e;
            }

            // assert
            Assert.IsNotNull(caught);
            Assert.AreEqual("Id<T> cannot be null, default or empty", caught.Message);
        }

        [TestMethod]
        public void NonEmptyKeyStringWorks()
        {
            // act
            var id = new Id<Id_2_Constructor_Test, string>("hi");

            // assert
            Assert.AreEqual("hi", id.Value);
        }

        [TestMethod]
        public void EmptyArrayIsNotAllowed()
        {
            // arrange
            var caught = default(ArgumentException);

            // act
            try
            {
                var id = new Id<Id_2_Constructor_Test, byte[]>(new byte[0]);
            }
            catch (ArgumentException e)
            {
                caught = e;
            }

            // assert
            Assert.IsNotNull(caught);
            Assert.AreEqual("Id<T> cannot be null, default or empty", caught.Message);
        }

        [TestMethod]
        public void NonEmptyArrayWorks()
        {
            // act
            var id = new Id<Id_2_Constructor_Test, byte[]>(new byte[1] { new byte() });

            // assert
            CollectionAssert.AreEquivalent(new byte[1] { new byte() }, id.Value);
        }
    }
}