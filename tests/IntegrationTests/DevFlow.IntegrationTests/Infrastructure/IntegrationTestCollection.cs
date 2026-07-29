namespace DevFlow.IntegrationTests.Infrastructure;

[CollectionDefinition(Name)]
public sealed class IntegrationTestCollection
    : ICollectionFixture<DevFlowWebApplicationFactory>
{
    public const string Name = "DevFlow integration tests";
}
