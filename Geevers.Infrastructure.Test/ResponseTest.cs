namespace Geevers.Infrastructure.Test
{
    using System;
    using System.Net;

    using Xunit;

    public class ResponseTest
    {
        public enum IHaveValuesThatDoNotMapToHttpStatusCodes
        {
            ImATeapot,
        };

        [Fact]
        public void AllValuesMustMapToHttpStatusCodes()
        {
            // arrange
            TypeInitializationException multipleValuesForOk = null;

            // act
            try
            {
                Response<IHaveValuesThatDoNotMapToHttpStatusCodes, int> response = 42;
            }
            catch (TypeInitializationException e) when (e.InnerException is NotSupportedException)
            {
                multipleValuesForOk = e;
            }

            // assert
            Assert.NotNull(multipleValuesForOk);
            Assert.Contains("cannot be mapped to", multipleValuesForOk.InnerException.Message);
        }

        public enum DeleteCarStatusCode
        {
            CarIsDefaultConflict,
            CarStillInUseConflict,
        }

        [Fact]
        public void SuccessfullCustomResponseIsProperlyConstructed()
        {
            // arrange
            // act
            Response<DeleteCarStatusCode, int> response = 42;

            // assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);

            Assert.Equal(42, response.Result);
        }

        [Fact]
        public void UnsuccessfullCustomResponseIsProperlyConstructed()
        {
            // arrange
            // act
            Response<DeleteCarStatusCode, int> response = DeleteCarStatusCode.CarStillInUseConflict;

            // assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Conflict, response.HttpStatusCode);

            Assert.Equal(DeleteCarStatusCode.CarStillInUseConflict, response.StatusCode);
        }
    }
}