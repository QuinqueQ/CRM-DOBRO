using Domain.Result.ErrorMessage;

namespace Application.Services;

public class ContactService(IContactRepository contactRepository, IUnitOfWork uow)
{
    public async Task<Result<List<ContactGetDTO>>> GetAllAsync()
    {
        List<Contact> contacts = await contactRepository.GetAllAsync();

        List<ContactGetDTO> contactsDTO = contacts.Adapt<List<ContactGetDTO>>();

        return contactsDTO;
    }

    public async Task<Result<List<ContactGetDTO>>> GetLeadsAsync()
    {
        var contactLeads = await contactRepository.GetContactLeadsAsync();

        List<ContactGetDTO> contactsDTO = contactLeads.Adapt<List<ContactGetDTO>>();
        return contactsDTO;
    }

    public async Task<Result> CreateContactAsync(ContactSetDTO contact, int marketingId)
    {
        Contact newContact = contact.Adapt<Contact>();

        newContact.MarketingId = marketingId;
        newContact.DateOfLastChanges = DateTime.Now;

        await contactRepository.AddAsync(newContact);
        await uow.SaveChangesAsync();

        return Result.Ok<bool>();
    }

    public async Task<Result<ContactGetDTO>> ContactUpdateAsync(ContactSetDTO contact, int contactId)
    {
        Contact? contactToChange = await contactRepository.GetByIdAsync(contactId);

        if (contactToChange is null) { return new Error(ContactErrors.IdNotFound, "Attempt to update a contact");}

        contactToChange = contact.Adapt(contactToChange);
        contactToChange.DateOfLastChanges = DateTime.Now;

        contactRepository.Update(contactToChange);
        await uow.SaveChangesAsync();
        ContactGetDTO getcontactdto = contactToChange.Adapt<ContactGetDTO>();


        return getcontactdto;
    }


    public async Task<Result<bool>> ContactChangeStatusAsync(ContactStatus status, int contactId)
    {
        var contact = await contactRepository.GetByIdAsync(contactId);
        if (contact is null)
            return new Error(ContactErrors.IdNotFound, "Attempt to update a contact");

        contact.Status = status;
        contact.DateOfLastChanges = DateTime.Now;
        contactRepository.Update(contact);
        await uow.SaveChangesAsync();
        return true;
    }
}
