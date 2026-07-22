using EquillibriumERP.Purchasing.Contracts;

namespace EquillibriumERP.Purchasing.Interfaces;

public interface ISupplierService
{
    Task<SupplierDto> CreateAsync(
        CreateSupplierRequest request,
        CancellationToken ct = default);

    Task<List<SupplierDto>> GetAllAsync();

    Task<SupplierDto?> GetByIdAsync(Guid id);

    Task<SupplierDto?> UpdateAsync(
        Guid id,
        UpdateSupplierRequest request,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id);
}