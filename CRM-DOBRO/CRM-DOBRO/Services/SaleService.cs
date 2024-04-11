using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using Mapster;
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

            var salesDTO = sales.Adapt<List<SaleGetDTO>>();

            foreach (Sale sale in sales)
            {
                var saleDTO = salesDTO.FirstOrDefault(dto => dto.Id == sale.Id); 
                if (saleDTO != null)
                    saleDTO.LeadtFullName = $"{sale.Lead?.Contact?.Name} {sale.Lead?.Contact?.Surname} {sale.Lead?.Contact?.LastName}";
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
            List<SaleGetDTO> salesDTO = sales.Adapt<List<SaleGetDTO>>();

            foreach (Sale sale in sales)
            {
                var saleDTO = salesDTO.FirstOrDefault(dto => dto.Id == sale.Id);
                if (saleDTO != null)
                    saleDTO.LeadtFullName = $"{sale.Lead?.Contact?.Name} {sale.Lead?.Contact?.Surname} {sale.Lead?.Contact?.LastName}";
            }

            return salesDTO;
        }

        public async Task<Sale?> CreateSaleAsync(int salerId,SaleSetDTO sale)
        {
            var leadFound = await _context.Leads.FirstAsync(l => l.Id == sale.LeadId);
            if (leadFound == null)
                return null;

            Sale newSale = sale.Adapt<Sale>();
            newSale.SalerId = salerId;
           
            _context.Add(newSale);
            await _context.SaveChangesAsync();

            return newSale;
        }

    }
}
