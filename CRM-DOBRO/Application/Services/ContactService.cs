using Domain.Abstractions.Repositories;

namespace Application.Services;

public class ContactService(IContactRepository contactRepository, IUnitOfWork uow)
{
    public async Task<List<ContactGetDTO>> GetContactsAsync()
    {
        List<Contact> contacts = await contactRepository.GetAllAsync();

        List<ContactGetDTO> contactsDTO = contacts.Adapt<List<ContactGetDTO>>();

        return contactsDTO;
    }

    public async Task<List<ContactGetDTO>> GetContactLeadsAsync()
    {
        var contactLeads = await contactRepository.GetContactLeadsAsync();

        List<ContactGetDTO> contactsDTO = contactLeads.Adapt<List<ContactGetDTO>>();
        return contactsDTO;
    }

    public async Task CreateContactAsync(ContactSetDTO contact, int marketingId)
    {
        Contact newContact = contact.Adapt<Contact>();

        newContact.MarketingId = marketingId;
        newContact.DateOfLastChanges = DateTime.Now;

        await contactRepository.AddAsync(newContact);
        await uow.SaveChangesAsync();
    }

    public async Task ContactChangeAsync(ContactSetDTO contact, int contactId)
    {
        Contact? contactToChange = await contactRepository.GetByIdAsync(contactId);

        contactToChange = contact.Adapt(contactToChange);
        contactToChange.DateOfLastChanges = DateTime.Now;

        contactRepository.Update(contactToChange);
        await uow.SaveChangesAsync();
    }


    public async Task ContactChangeStatusAsync(ContactStatus status, int contactId)
    {
        var contact = await contactRepository.GetByIdAsync(contactId);
        contact.Status = status;
        contact.DateOfLastChanges = DateTime.Now;
        contactRepository.Update(contact);
        await uow.SaveChangesAsync();
    }
}
