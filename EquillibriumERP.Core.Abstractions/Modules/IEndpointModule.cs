using Microsoft.AspNetCore.Routing;
namespace EquillibriumERP.Core.Abstractions.Modules;

public interface IEndpointModule
{
    void MapEndpoints(IEndpointRouteBuilder endpoints);
}