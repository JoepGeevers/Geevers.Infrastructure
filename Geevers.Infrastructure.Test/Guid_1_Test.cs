namespace Geevers.Infrastructure.Test
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Guid_1_Test
    {
        [TestMethod]
        public void IdCanBeSerialized()
        {
            // arrange
            var id = new Guid<Guid_1_Test>(Guid.NewGuid());

            // act
            new BinaryFormatter().Serialize(new MemoryStream(), id);
        }
    }
}
