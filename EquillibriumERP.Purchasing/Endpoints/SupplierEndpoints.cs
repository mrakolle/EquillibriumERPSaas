using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Purchasing.Contracts;

namespace EquillibriumERP.Purchasing.Endpoints;

public static class SupplierEndpoints
{
    public static void MapSupplierEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/purchasing/suppliers")
            .WithTags("Purchasing - Suppliers");

        group.MapPost("/", async (
            CreateSupplierRequest request,
            PurchasingDbContext db,
            CancellationToken ct) =>
        {
            var supplier = new Supplier
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
}