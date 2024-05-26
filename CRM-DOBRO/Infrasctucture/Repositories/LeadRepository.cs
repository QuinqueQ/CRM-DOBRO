namespace Infrasctucture.Repositories;

public class LeadRepository(CRMDBContext dbContext) : BaseRepository<Lead>(dbContext), ILeadRepository
{
    public async Task<List<Lead>> GetLeadsBySalerIdAsync(int salerId)
    {
        return await DBcontext.Leads
            .Include(l => l.Saler)
            .Include(l => l.Contact)
            .Where(l => l.SalerId == salerId)
            .ToListAsync();
    }

    public async Task<Contact?> FoundContactLeadAsync(int contactId)
    {
        Contact? contact = await DBcontext.Contacts.Where(c => c.Status == ContactStatus.Lead).FirstAsync(c => c.Id == contactId);

        if (contact == null)
            return null;

        return contact;
    }

}
