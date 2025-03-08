using PhonebookSystem.Core.Entities;

namespace PhonebookSystem.Service.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact?> GetContactByIdAsync(int id);
        Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm);
        Task<Contact> AddContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(int id);
    }
}