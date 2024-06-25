using TechAssess.SupplierService.Dto;
using TechAssess.SupplierService.Models;
using TechAssess.SupplierService.Util;

namespace TechAssess.SupplierService.Services
{
    public interface ISupplierService
    {
        Task<PaginatedList<Supplier>> GetAsync(int pageIndex, int pageSize, string? legalName, string? tradeName, string? taxIdentificationNumber, int? countryId);
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<Supplier?> UpdateAsync(Supplier supplier, int id);
        Task DeleteAsync(int id);
    }
}
