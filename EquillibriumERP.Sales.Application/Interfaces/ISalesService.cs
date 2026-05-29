using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquillibriumERP.Sales.Application.DTOs;

namespace EquillibriumERP.Sales.Application.Interfaces;

public interface ISalesService
{
    Task<SalesDto> CreateAsync(CreateSalesDto dto);

    Task<List<SalesDto>> GetAllAsync();

    Task<SalesDto?> GetByIdAsync(Guid id);

    Task<SalesDto?> UpdateAsync(Guid id, UpdateSalesDto dto);

    Task<bool> DeleteAsync(Guid id);
}





