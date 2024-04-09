using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Services
{
    public class LeadService(CRMDBContext context)
    {
        private readonly CRMDBContext _context = context;
        public async Task<List<LeadGetDTO>> GetMyLeadsAsync(int salerId)
        {
            List<Lead> leads = await _context.Leads
                .Where(l => l.SalerId == salerId)
                .ToListAsync();
            List<LeadGetDTO> leadsDTO = [];

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

        public async Task<bool> CreateLeadAsync(LeadSetDTO newLead, int salerId)
        {
            Lead lead = new()
            {
                ContactId = newLead.ContactId,
                SalerId = salerId,
                Status = newLead.Status,
            };

            var contact = await _context.Contacts.FirstAsync(c => c.Id == newLead.ContactId);
            if (contact == null)
                return false;

            contact.Status = ContactStatus.Lead;
            _context.Update(contact);
            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Lead?> ChangeLeadStatusAsync(int leadId, LeadStatus status)
        {

            Lead? lead = await _context.Leads.FirstAsync(l => l.Id == leadId);
       
            lead.Status = status;
            _context.Update(lead);
            await _context.SaveChangesAsync();
            return lead;
        }

    }
}
