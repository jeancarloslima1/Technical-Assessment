using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TechAssess.SupplierService.Data;
using TechAssess.SupplierService.Models;

namespace TechAssess.SupplierService.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataContext _context;
        private readonly IMemoryCache _cache;


        public CountryService(DataContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Country>> GetAsync()
        {
            if (!_cache.TryGetValue("Countries", out List<Country> countries))
            {
                countries = await _context.Countries.ToListAsync();

                _cache.Set("Countries", countries, TimeSpan.FromDays(1));
            }

            return countries;
        }
    }
}
