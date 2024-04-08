using CRM_DOBRO.Data;

namespace CRM_DOBRO.Services
{
    public class LeadService
    {
        private readonly CRMDBContext _context;

        public LeadService(CRMDBContext context)
        {
            this._context = context;
        }

    }
}
