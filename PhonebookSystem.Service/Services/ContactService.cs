using PhonebookSystem.Core.Entities;
using PhonebookSystem.Core.Interfaces;
using PhonebookSystem.Service.Interfaces;

namespace PhonebookSystem.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            return await _contactRepository.AddContactAsync(contact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            return await _contactRepository.DeleteContactAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllContactsAsync();
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            return await _contactRepository.GetContactByIdAsync(id);
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
        {
            return await _contactRepository.SearchContactsAsync(searchTerm);
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            return await _contactRepository.UpdateContactAsync(contact);
        }
    }
}