using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DevFlow.IntegrationTests.Infrastructure;

namespace DevFlow.IntegrationTests.Api;

[Collection(IntegrationTestCollection.Name)]
public sealed class HealthChecksTests(DevFlowWebApplicationFactory factory)
{
    [Fact]
    public async Task HealthEndpointReturnsHealthyReport()
    {
        using HttpClient client = factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync(
            "/health",
            TestContext.Current.CancellationToken);
        using JsonDocument report =
            await response.Content.ReadFromJsonAsync<JsonDocument>(
                TestContext.Current.CancellationToken)
            ?? throw new InvalidOperationException("Health report response was empty.");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", report.RootElement.GetProperty("status").GetString());
    }
}
