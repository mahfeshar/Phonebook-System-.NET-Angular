using Microsoft.EntityFrameworkCore;
using PhonebookSystem.Core.Entities;
using PhonebookSystem.Core.Interfaces;
using PhonebookSystem.Repository.Data;

namespace PhonebookSystem.Repository.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return await _context.Contacts
                .Where(c => c.Name.ToLower().Contains(searchTerm) ||
                           c.PhoneNumber.ToLower().Contains(searchTerm) ||
                           c.Email.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            var existingContact = await _context.Contacts.FindAsync(contact.Id);
            if (existingContact == null) return false;

            existingContact.Name = contact.Name;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Email = contact.Email;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}