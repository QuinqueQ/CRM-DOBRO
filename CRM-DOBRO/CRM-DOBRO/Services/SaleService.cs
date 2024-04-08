using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Services
{
    public class SaleService(CRMDBContext context)
    {
        private readonly CRMDBContext _context = context;

        public async Task<List<Sale>> GetSalesAsync()
        {
            List<Sale>? sales = await _context.Sales.ToListAsync();
            return sales;
        }

        public async Task<List<Sale>> GetMySalesAsync(int salerId)
        {
            List<Sale> Sales = await _context.Sales.Where(s => s.SalerId == salerId).ToListAsync();
            return Sales;
        }

        public async Task CreateSaleAsync(int salerId,SaleSetDTO sale)
        {
            Sale newSale = new()
            {
                SalerId = salerId,
                DateOfSale = DateTime.Now,
                LeadId = sale.LeadId,
            };
            _context.Add(newSale);
            await _context.SaveChangesAsync();
        }

    }
}
