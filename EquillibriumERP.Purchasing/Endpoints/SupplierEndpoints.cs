using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Purchasing.Contracts;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Purchasing.Interfaces;

namespace EquillibriumERP.Purchasing.Endpoints;

public static class SupplierEndpoints
{
    public static void MapSupplierEndpoints(RouteGroupBuilder app)
    {
        //app.MapGroup("/api/purchasing/suppliers");

        app.MapPost("/Suppliers/New", async (
            CreateSupplierRequest dto,
            HttpContext ctx,
            CancellationToken ct) =>
        {
            {
            var service = ctx.RequestServices.GetRequiredService<ISupplierService>();
            var result = await service.CreateAsync(dto,ct);

            return Results.Ok(result);
            }
        });
    }
}

           /* var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                SupplierCode = request.SupplierCode,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ContactPerson = request.ContactPerson,
                IsActive = true
            };

            db.Suppliers.Add(supplier);
            await db.SaveChangesAsync(ct);

            return Results.Ok(new SupplierResponse
            {
                Id = supplier.Id,
                SupplierCode = supplier.SupplierCode,
                Name = supplier.Name,
                IsActive = supplier.IsActive
            });
        });
    }
}*/