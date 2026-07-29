using DevFlow.Common.Domain;

namespace DevFlow.UnitTests.Common.Domain;

public sealed class ResultTests
{
    [Fact]
    public void SuccessCreatesSuccessfulResult()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void FailureCreatesFailedResult()
    {
        var error = Error.Failure("Test.Failure", "A test failure");

        var result = Result.Failure(error);

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void AccessingFailedResultValueThrowsInvalidOperationException()
    {
        var result = Result.Failure<string>(
            Error.Failure("Test.Failure", "A test failure"));

        Assert.Throws<InvalidOperationException>(() => result.Value);
    }
}
