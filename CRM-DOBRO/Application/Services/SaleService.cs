using Domain.Abstractions.Repositories;

namespace Application.Services;

public class SaleService(ISaleRepository saleRepository, IUnitOfWork uow)
{
    public async Task<List<SaleGetDTO>> GetSalesAsync()
    {
        List<Sale> sales = await saleRepository.GetAllAsync();

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
        List<Sale> sales = await saleRepository.GetSalesBySalerIdAsync(salerId);

        List<SaleGetDTO> salesDTO = sales.Adapt<List<SaleGetDTO>>();

        foreach (Sale sale in sales) // We assign the FullName of the lead manually, since Mapster cannot assign them itself
        {
            var saleDTO = salesDTO.FirstOrDefault(dto => dto.Id == sale.Id);
            if (saleDTO != null)
                saleDTO.LeadtFullName = $"{sale.Lead?.Contact?.Name} {sale.Lead?.Contact?.Surname} {sale.Lead?.Contact?.LastName}";
        }

        return salesDTO;
    }

    public async Task<Sale?> CreateSaleAsync(int salerId,SaleSetDTO sale)
    {
        Lead? leadFound = await saleRepository.FoundLeadAsync(sale.LeadId);
        if (leadFound == null)
            return null;
        Sale newSale = sale.Adapt<Sale>();
        newSale.SalerId = salerId;
       
        await saleRepository.AddAsync(newSale);
        await uow.SaveChangesAsync();

        return newSale;
    }

}
