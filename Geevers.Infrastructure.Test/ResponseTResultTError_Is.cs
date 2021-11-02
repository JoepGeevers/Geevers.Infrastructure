namespace Geevers.Infrastructure.Test
{
	using System;
    using System.Linq;
    using System.Net;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class ResponseTResultTError_Is
	{
		[TestMethod]
		public void IfResponseWithResultStatus_DoesNot_MatchCondition_Is_ReturnsFalseAndOutVarsAreSet()
		{
			// arrange
			Response<int, Error> response = (HttpStatusCode.Created, 42);

			var statii = Enum.GetValues(typeof(HttpStatusCode))
				.Cast<HttpStatusCode>()
				.Where(s => s != HttpStatusCode.Created);

			// act
			var responses = statii
				.Select(cue =>
				{
					var a = response.Is(cue, out var result, out var status, out var error);

					return (InStatus: cue, IsResult: a, OutResult: result, OutStatus: cue, OutError: error);
				});

			// assert
			Assert.IsTrue(responses.All(p => p.InStatus == p.OutStatus));
			Assert.IsTrue(responses.All(p => false == p.IsResult));
			Assert.IsTrue(responses.All(p => p.OutResult == 42));
			Assert.IsTrue(responses.All(p => p.OutError == Error.Unknown));
		}

		[TestMethod]
		public void IfResponseWithErrorStatus_DoesNot_MatchCondition_Is_ReturnsFalseAndOutVarsAreSet()
		{
			// arrange
			Response<int, Error> response = (HttpStatusCode.Conflict, Error.UniverseExploded);

			var statii = Enum.GetValues(typeof(HttpStatusCode))
				.Cast<HttpStatusCode>()
				.Where(s => s != HttpStatusCode.Conflict);

			// act
			var responses = statii
				.Select(cue =>
				{
					var a = response.Is(cue, out var result, out var status, out var error);

					return (InStatus: cue, IsResult: a, OutResult: result, OutStatus: cue, OutError: error);
				});

			// assert
			Assert.IsTrue(responses.All(p => p.InStatus == p.OutStatus));
			Assert.IsTrue(responses.All(p => false == p.IsResult));
			Assert.IsTrue(responses.All(p => p.OutResult == default));
			Assert.IsTrue(responses.All(p => p.OutError == Error.UniverseExploded));
		}

		[TestMethod]
		public void IfResponseWithResultStatus_Does_MatchCondition_Is_ReturnsTrueAndOutVarsAreSet()
		{
			// arrange
			Response<int, Error> response = (HttpStatusCode.Created, 42);

			// act
			var a = response.Is(HttpStatusCode.Created, out var result, out var status, out var error);

			// assert
			Assert.IsTrue(a);

			Assert.AreEqual(42, result);
			Assert.AreEqual(HttpStatusCode.Created, status);
			Assert.AreEqual(Error.Unknown, error);
		}

		[TestMethod]
		public void IfResponseWithErrorStatus_Does_MatchCondition_Is_ReturnsTrueAndOutVarsAreSet()
		{
			// arrange
			Response<int, Error> response = (HttpStatusCode.Conflict, Error.UniverseExploded);

			// act
			var a = response.Is(HttpStatusCode.Conflict, out var result, out var status, out var error);

			// assert
			Assert.IsTrue(a);

			Assert.AreEqual(default, result);
			Assert.AreEqual(HttpStatusCode.Conflict, status);
			Assert.AreEqual(Error.UniverseExploded, error);
		}

		public enum Error
		{
			Unknown = 0,
			UniverseExploded = 1,
		}
	}
}
