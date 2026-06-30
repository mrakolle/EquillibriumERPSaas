using EquillibriumERP.Purchasing.Contracts;

namespace EquillibriumERP.Purchasing.Interfaces;
public interface ISupplierService
{
    Task<SupplierDto> CreateAsync(CreateSupplierRequest dto, CancellationToken ct);

    Task<List<SupplierDto>> GetAllAsync();

    Task<SupplierDto?> GetByIdAsync(Guid id);

    Task<SupplierDto?> UpdateAsync(Guid id, UpdateSupplierRequest dto);

    Task<bool> DeleteAsync(Guid id);
}