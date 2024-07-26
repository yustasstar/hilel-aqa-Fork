namespace Lesson21;

public class AsyncHomework
{
    public async Task<string> GetStringAsync()
    {
        await Task.Delay(500);
        return "Hello, World!";
    }

    public async Task<int> GetNumberWithExceptionAsync()
    {
        await Task.Delay(1000);
        throw new InvalidOperationException("An error occurred while fetching the number.");
    }

    [Test]
    public async Task TestGetStringAsync()
    {
        // TODO: Uncomment and implement test so it pass
        var expected = "Hello, World!";
        var result = await GetStringAsync();
        Assert.That(result, Is.EqualTo(expected), $"{result} is not equal to {expected}");
    }

    [Test]
    public void TestGetNumberWithExceptionAsync()
    {
        // TODO: Verify that GetNumberWithExceptionAsync() throws InvalidOperationException
        // and that exception message is "An error occurred while fetching the number."
        Assert.Multiple(() =>
        {
            {
                // Act & Assert
                var exception = 
                Assert.ThrowsAsync<InvalidOperationException>( () => GetNumberWithExceptionAsync());

                // Verify the exception message
                var message = "An error occurred while fetching the number.";
                Assert.That(exception?.Message, Is.EqualTo(message), $"Messages:{message} is not equal to {exception}");
            }   
        });
    }

}