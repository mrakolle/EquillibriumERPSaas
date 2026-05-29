using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Sales.Application.DTOs;
using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Products.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Infrastructure.Services;

public class SalesService : ISalesService
{
    private readonly ITenantDbContext _db;
    private readonly ITenantResolver _tenantResolver;

    public SalesService(
        ITenantDbContext db,
        ITenantResolver tenantResolver)
    {
        _db = db;
        _tenantResolver = tenantResolver;
    }


    public Task<SalesDto> CreateAsync(CreateSalesDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<SalesDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SalesDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<SalesDto?> UpdateAsync(Guid id, UpdateSalesDto dto)
    {
        throw new NotImplementedException();
    }
}