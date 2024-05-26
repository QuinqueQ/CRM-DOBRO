namespace Infrasctucture.Repositories;

public class SaleRepository(CRMDBContext dbContext) : BaseRepository<Sale>(dbContext), ISaleRepository
{
    public async Task<List<Sale>> GetSalesBySalerIdAsync(int salerId)
    {
        return await DBcontext.Sales
           .Include(s => s.Lead)
           .ThenInclude(l => l.Contact)
           .Include(s => s.Saler)
           .Where(s => s.SalerId == salerId)
           .ToListAsync();
    }

    public override async Task<List<Sale>> GetAllAsync()
    {
        return await DBcontext.Sales
      .Include(s => s.Lead)
      .ThenInclude(l => l.Contact)
      .Include(s => s.Saler)
      .ToListAsync();
    }

    public async Task<Lead?> FoundLeadAsync(int leadId)
    {
        Lead? leadFound = await DBcontext.Leads.FirstAsync(l => l.Id == leadId);
        if (leadFound == null)
            return null;
        return leadFound;
    }
}
