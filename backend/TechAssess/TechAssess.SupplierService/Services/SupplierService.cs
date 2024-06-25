using TechAssess.SupplierService.Data;
using TechAssess.SupplierService.Models;
using TechAssess.SupplierService.Util;

namespace TechAssess.SupplierService.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly DataContext _context;

        public SupplierService(DataContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Supplier>> GetAsync(int pageIndex, int pageSize, string? legalName, string? tradeName, string? taxIdentificationNumber, int? countryId)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(legalName))
            {
                query = query.Where(c => c.LegalName.Contains(legalName));
            }

            if (!string.IsNullOrEmpty(tradeName))
            {
                query = query.Where(c => c.TradeName.Contains(tradeName));
            }

            if (!string.IsNullOrEmpty(taxIdentificationNumber))
            {
                query = query.Where(c => c.TaxIdentificationNumber == taxIdentificationNumber);
            }

            if (countryId > 0)
            {
                query = query.Where(c => c.CountryId == countryId);
            }

            query = query.OrderByDescending(c => c.LastEdited);

            return await PaginatedList<Supplier>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return supplier;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Supplier?> UpdateAsync(Supplier supplier, int id)
        {
            var dbSupplier = await _context.Suppliers.FindAsync(id);
            if (dbSupplier == null) throw new Exception("Supplier not found");

            _context.Entry(dbSupplier).CurrentValues.SetValues(supplier);
            await _context.SaveChangesAsync();
            return dbSupplier;
        }
        public async Task DeleteAsync(int id)
        {
            var dbSupplier = await _context.Suppliers.FindAsync(id);
            if (dbSupplier == null) throw new Exception("Supplier not found");

            _context.Remove(dbSupplier);
            await _context.SaveChangesAsync();
        }

    }
}
