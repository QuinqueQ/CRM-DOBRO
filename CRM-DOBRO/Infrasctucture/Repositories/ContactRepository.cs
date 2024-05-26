namespace Infrasctucture.Repositories;

public class ContactRepository(CRMDBContext dbContext) : BaseRepository<Contact>(dbContext) ,IContactRepository
{
    public async Task<List<Contact>> GetContactLeadsAsync()
    {
        var contactLeads = await DBcontext.Contacts
            .Include(c => c.Marketing)
            .Where(c => c.Status == ContactStatus.Lead)
            .ToListAsync();
        return contactLeads;
    }

    public override Task<List<Contact>> GetAllAsync()
    {
        return DBcontext.Set<Contact>()
            .AsNoTracking()
            .Include(c => c.Marketing)
            .ToListAsync();
    }
}
