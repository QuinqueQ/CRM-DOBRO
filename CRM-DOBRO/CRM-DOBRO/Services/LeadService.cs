using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Services
{
    public class LeadService(CRMDBContext context)
    {
        private readonly CRMDBContext _context = context;
        public async Task<List<LeadGetDTO>> GetMyLeadsAsync(int salerId)
        {
            List<Lead> leads = await _context.Leads
                .Include(l => l.Saler)
                .Include(l => l.Contact)
                .Where(l => l.SalerId == salerId)
                .ToListAsync();

            List<LeadGetDTO> leadsDTO = leads.Adapt<List<LeadGetDTO>>();

            foreach (var lead in leads)
            {
                LeadGetDTO? leadDTO = leadsDTO.FirstOrDefault(dto => dto.Id == lead.Id);
                if (leadDTO != null)
                {
                    leadDTO.ContactFullName = lead.Contact?.Name + " " + lead.Contact?.Surname + " " + lead.Contact?.LastName;
                    leadDTO.SalertFullName = lead.Saler.FullName;
                }
            }

            return leadsDTO;
        }

        public async Task<bool> CreateLeadAsync(LeadSetDTO newLead, int salerId)
        {
            Lead lead = newLead.Adapt<Lead>();
            lead.SalerId = salerId;

            var contact = await _context.Contacts.Where(c => c.Status == ContactStatus.Lead).FirstAsync(c => c.Id == newLead.ContactId);
            if (contact == null)
                return false;

            contact.Status = ContactStatus.Lead;
            _context.Update(contact);
            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LeadGetDTO?> ChangeLeadStatusAsync(int leadId, LeadStatus status)
        {

            Lead? lead = await _context.Leads.FirstAsync(l => l.Id == leadId);
            lead.Status = status;
            _context.Update(lead);
            await _context.SaveChangesAsync();
            LeadGetDTO leadDTO = lead.Adapt<LeadGetDTO>();
            return leadDTO;
        }

    }
}
