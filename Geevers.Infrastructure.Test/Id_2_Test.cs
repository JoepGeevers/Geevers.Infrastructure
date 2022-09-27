namespace Geevers.Infrastructure.Test
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Id_2_Test
    {
        [TestMethod]
        public void IdCanBeSerialized()
        {
            // arrange
            var id = new Id<Id_2_Test, int>(123);

            // act
            new BinaryFormatter().Serialize(new MemoryStream(), id);
        }
    }
}