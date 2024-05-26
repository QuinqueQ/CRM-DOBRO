namespace Infrasctucture.Repositories
{
#pragma warning disable CS9107 
    public class SaleRepository(CRMDBContext dbContext) : BaseRepository<Sale>(dbContext), ISaleRepository
#pragma warning restore CS9107 
    {
        public async Task<List<Sale>> GetSalesBySalerIdAsync(int salerId)
        {
            return await dbContext.Sales
               .Include(s => s.Lead)
               .ThenInclude(l => l.Contact)
               .Include(s => s.Saler)
               .Where(s => s.SalerId == salerId)
               .ToListAsync();
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await dbContext.Sales
              .Include(s => s.Lead)
              .ThenInclude(l => l.Contact)
              .Include(s => s.Saler)
              .ToListAsync();
        }

        public async Task<Lead?> FoundLeadAsync(int leadId)
        {
            Lead? leadFound = await dbContext.Leads.FirstAsync(l => l.Id == leadId);
            if (leadFound == null)
                return null;
            return leadFound;
        }
    }
}
