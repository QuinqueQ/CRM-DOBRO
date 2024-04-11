using CRM_DOBRO.CustomAttributes;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace CRM_DOBRO.Services
{
    public class ContactService(CRMDBContext context)
    {
        private readonly CRMDBContext _context = context;


        public async Task<List<ContactGetDTO>> GetContactsAsync()
        {
            List<Contact> contacts = await _context.Contacts
                .Include(c => c.Marketing)
                .ToListAsync();

            List<ContactGetDTO> contactsDTO = contacts.Adapt<List<ContactGetDTO>>();

            return contactsDTO;
        }

        public async Task<List<ContactGetDTO>> GetContactLeadsAsync()
        {
            var leads = await _context.Contacts
                .Include(c => c.Marketing)
                .Where(c => c.Status == ContactStatus.Lead)
                .ToListAsync();

            List<ContactGetDTO> contactsDTO = leads.Adapt<List<ContactGetDTO>>();
            return contactsDTO;
        }

        public async Task CreateContactAsync(ContactSetDTO contact, int marketingId)
        {
            Contact newContact = contact.Adapt<Contact>();

            newContact.MarketingId = marketingId;
            newContact.DateOfLastChanges = DateTime.Now;

            _context.Add(newContact);
            await _context.SaveChangesAsync();
        }

        public async Task ContactChangeAsync(ContactSetDTO contact, int contactId)
        {
            Contact? contactToChange = await _context.Contacts.FirstAsync(c => c.Id == contactId);

            contactToChange = contact.Adapt(contactToChange);
            contactToChange.DateOfLastChanges = DateTime.Now;

            _context.Update(contactToChange);
            await _context.SaveChangesAsync();
        }


        public async Task ContactChangeStatusAsync(ContactStatus status, int contactId)
        {
            var contact = await _context.Contacts.FirstAsync(c => c.Id == contactId);
            contact.Status = status;
            contact.DateOfLastChanges = DateTime.Now;
            _context.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
