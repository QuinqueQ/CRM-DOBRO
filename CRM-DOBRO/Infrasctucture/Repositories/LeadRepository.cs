namespace Infrasctucture.Repositories
{
#pragma warning disable CS9107
    public class LeadRepository(CRMDBContext dbContext) : BaseRepository<Lead>(dbContext), ILeadRepository
#pragma warning restore CS9107 
    {
        public async Task<List<Lead>> GetLeadsBySalerIdAsync(int salerId)
        {
            return await dbContext.Leads
                .Include(l => l.Saler)
                .Include(l => l.Contact)
                .Where(l => l.SalerId == salerId)
                .ToListAsync();
        }

        public async Task<Contact?> FoundContactLeadAsync(int contactId)
        {
            Contact? contact = await dbContext.Contacts.Where(c => c.Status == ContactStatus.Lead).FirstAsync(c => c.Id == contactId);

            if (contact == null)
                return null;

            return contact;
        }

    }
}
