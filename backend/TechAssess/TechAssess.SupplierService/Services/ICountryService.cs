using TechAssess.SupplierService.Models;

namespace TechAssess.SupplierService.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetAsync();
    }
}
