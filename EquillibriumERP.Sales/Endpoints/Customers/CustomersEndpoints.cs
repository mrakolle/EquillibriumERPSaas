using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Sales.Contracts.Estimates;
using EquillibriumERP.Sales.Contracts.Customers;
using EquillibriumERP.Sales.Interfaces;

namespace EquillibriumERP.Sales.Endpoints;

public static class CustomersEndpoints
{
    public static void MapCustomersEndpoints(IEndpointRouteBuilder app)
    {
        //MapGetCustomerById(app);
        MapCreateCustomer(app);
        
    }

    private static void MapCreateCustomer(IEndpointRouteBuilder app)
    {
        app.MapPost("/customer",
        async (
            CreateCustomerRequest request,
            ICustomerService service,
            CancellationToken ct) =>
        {
            var customer = await service.CreateAsync(request, ct);

            return Results.Created(
                $"/api/sales/customers/{customer.Id}",
                customer);
        });
        }
}