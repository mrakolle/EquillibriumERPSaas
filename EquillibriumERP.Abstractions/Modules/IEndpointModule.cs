using Microsoft.AspNetCore.Routing;
namespace EquillibriumERP.Abstractions.Modules;

public interface IEndpointModule
{
    void MapEndpoints(IEndpointRouteBuilder endpoints);
}