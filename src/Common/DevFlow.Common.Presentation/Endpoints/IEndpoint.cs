using Microsoft.AspNetCore.Routing;

namespace DevFlow.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
