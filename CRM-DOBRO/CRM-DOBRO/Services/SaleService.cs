using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Services
{
    public class SaleService(CRMDBContext context)
    {
        private readonly CRMDBContext _context = context;

        public async Task<List<SaleGetDTO>> GetSalesAsync()
        {
            List<Sale> sales = await _context.Sales
                .Include(s => s.Saler)
                .Include(s => s.Lead)
                .ThenInclude(l => l.Contact)
                .ToListAsync();
            List<SaleGetDTO> salesDTO = [];
            foreach (Sale sale in sales)
            {
                SaleGetDTO saleDTO = new()
                {
                    Id = sale.Id,
                    LeadId = sale.LeadId,
                    LeadtFullName = sale.Lead?.Contact?.Name 
                    +" "+sale.Lead?.Contact?.Surname
                    +" "+sale.Lead?.Contact?.LastName,
                    SalerId = sale.SalerId,
                    SalerFullName = sale.Saler?.FullName,
                    DateOfSale = sale.DateOfSale,
                };
                salesDTO.Add(saleDTO);
            }
            return salesDTO;
        }

        public async Task<List<SaleGetDTO>> GetMySalesAsync(int salerId)
        {
            List<Sale> sales = await _context.Sales
                .Include(s => s.Lead)
                .ThenInclude(l => l.Contact)
                .Include(s => s.Saler)
                .Where(s => s.SalerId == salerId)
                .ToListAsync();
            List<SaleGetDTO> salesDTO = [];
            foreach (Sale sale in sales)
            {
                SaleGetDTO saleDTO = new()
                {
                    Id = sale.Id,
                    LeadId = sale.LeadId,
                    LeadtFullName = sale.Lead?.Contact?.Name
                    + " " + sale.Lead?.Contact?.Surname
                    + " " + sale.Lead?.Contact?.LastName,
                    SalerId = sale.SalerId,
                    SalerFullName = sale.Saler?.FullName,
                    DateOfSale = sale.DateOfSale,
                };
                salesDTO.Add(saleDTO);
            }
            return salesDTO;
        }

        public async Task<Sale?> CreateSaleAsync(int salerId,SaleSetDTO sale)
        {
            var leadFound = await _context.Leads.FirstAsync(l => l.Id == sale.LeadId);
            if (leadFound == null)
                return null;

            Sale newSale = new()
            {
                SalerId = salerId,
                DateOfSale = DateTime.Now,
                LeadId = sale.LeadId,
            };
            _context.Add(newSale);
            await _context.SaveChangesAsync();

            return newSale;
        }

    }
}
