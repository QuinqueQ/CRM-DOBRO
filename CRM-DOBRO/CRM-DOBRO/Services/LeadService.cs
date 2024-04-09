using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Services
{
    public class LeadService
    {
        private readonly CRMDBContext _context;

        public LeadService(CRMDBContext context)
        {
            this._context = context;
        }

        public async Task<List<LeadGetDTO>> GetMyLeadsAsync(int salerId)
        {
            List<Lead> leads = await _context.Leads
                .Where(l => l.SalerId == salerId)
                .ToListAsync();
            List<LeadGetDTO> leadsDTO = new List<LeadGetDTO>();

            foreach (var lead in leads)
            {
                LeadGetDTO leadDTO = new()
                {
                    Id = lead.Id,
                    ContactId = lead.ContactId,
                    SalerId = lead.SalerId,
                    Status = lead.Status,
                };
                leadsDTO.Add(leadDTO);
            }

            return leadsDTO;
        }

        public async Task CreateLeadAsync(LeadSetDTO newLead,int contactId, int salerId)
        {
            Lead lead = new()
            {
                ContactId = contactId,
                SalerId = salerId,
                Status = LeadStatus.New,
            };

            var contact = await _context.Contacts.FirstAsync(c => c.Id == contactId);
            contact.Status = ContactStatus.Lead;
            _context.Update(contact);
            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();
        }

        public async Task<Lead> ChangeLeadStatusAsync(int leadId, LeadStatus status)
        {

            Lead? lead = await _context.Leads.FirstAsync(l => l.Id == leadId);
       
            lead.Status = status;
            _context.Update(lead);
            await _context.SaveChangesAsync();
            return lead;
        }

    }
}
