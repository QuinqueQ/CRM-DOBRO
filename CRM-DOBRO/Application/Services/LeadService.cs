using Domain.Abstractions.Repositories;

namespace Application.Services
{
    public class LeadService(ILeadRepository leadRepository, IUnitOfWork uow, IContactRepository contactRepository)
    {
        public async Task<List<LeadGetDTO>> GetMyLeadsAsync(int salerId)
        {
            List<Lead> leads = await leadRepository.GetLeadsBySalerIdAsync(salerId);

            List<LeadGetDTO> leadsDTO = leads.Adapt<List<LeadGetDTO>>();

            foreach (var lead in leads)
            {
                LeadGetDTO? leadDTO = leadsDTO.FirstOrDefault(dto => dto.Id == lead.Id);
                if (leadDTO != null) // We assign the FOI of the contact and the salesperson manually, since Mapster cannot assign them itself
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

            Contact? contact = await leadRepository.FoundContactLeadAsync(salerId);
            if (contact == null)
                return false;

            contact.Status = ContactStatus.Lead;

            contactRepository.Update(contact);

            await leadRepository.AddAsync(lead);
            await uow.SaveChangesAsync();

            return true;
        }

        public async Task<LeadGetDTO?> ChangeLeadStatusAsync(int leadId, LeadStatus status)
        {
            Lead? lead = await leadRepository.GetByIdAsync(leadId);
            lead.Status = status;
            leadRepository.Update(lead);
            await uow.SaveChangesAsync();
            LeadGetDTO leadDTO = lead.Adapt<LeadGetDTO>();
            return leadDTO;
        }

    }
}
