using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Entities;
using CRM_DOBRO.Enums;
using Microsoft.EntityFrameworkCore;
namespace CRM_DOBRO.Services
{
    public class ContactService
    {
        private readonly CRMDBContext _context;

        public ContactService(CRMDBContext context)
        {
            this._context = context;
        }

        public async Task<List<ContactGetDTO>?> GetContactsAsync()
        {
           List<Contact> contacts = await _context.Contacts.ToListAsync();
           List<ContactGetDTO> contactsDTO = new();

            foreach (var contact in contacts)
            {
                ContactGetDTO contactDTO = new()
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Surname = contact.Surname,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Status = contact.Status,
                    MarketingId = contact.MarketingId,
                    DateOfLastChanges = contact.DateOfLastChanges,
                };
                contactsDTO.Add(contactDTO);            
            }
            return contactsDTO;
        }

        public async Task<List<Contact>?> GetLeadsAsync()
        {
            var leads = await _context.Contacts
                .Where(c => c.Status == ContactStatus.Lead)
                .ToListAsync();
            return leads;
        }

        public async Task CreateContactAsync(ContactSetDTO contact, int marketingId)
        {
            Contact newContact = new()
            {
                Name = contact.Name,
                Surname= contact.Surname,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Status= contact.Status,
                MarketingId = marketingId,
                DateOfLastChanges = contact.DateOfLastChanges,
            };
            _context.Add(newContact);
            await _context.SaveChangesAsync();
        }

        public async Task ContactChangeAsync(ContactSetDTO contact, int contactId)
        {
          var contactToChange = await _context.Contacts.FirstAsync(c => c.Id == contactId);
          
            contactToChange = new()
            {
                Name = contact.Name,
                Surname = contact.Surname,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Status = contact.Status,
                MarketingId = contactToChange.MarketingId,
                DateOfLastChanges = contact.DateOfLastChanges,
            };
            _context.Add(contactToChange);
            await _context.SaveChangesAsync();

        }

        public async Task ContactChangeStatusAsync(ContactStatus status, int contactId)
        {
            var contact = await _context.Contacts.FirstAsync(c => c.Id == contactId);
            contact.Status = status;
            _context.Add(contact);
            await _context.SaveChangesAsync();
        }
    }
}
