namespace Infrasctucture.Repositories
{
#pragma warning disable CS9107 
    public class ContactRepository(CRMDBContext dbContext) : BaseRepository<Contact>(dbContext) ,IContactRepository
#pragma warning restore CS9107 
    {
        public async Task<List<Contact>> FoundContactLeadsAsync()
        {
            var contactLeads = await dbContext.Contacts
                .Include(c => c.Marketing)
                .Where(c => c.Status == ContactStatus.Lead)
                .ToListAsync();
            return contactLeads;
        }
    }
}
