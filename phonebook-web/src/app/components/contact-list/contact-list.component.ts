import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Contact, ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.scss']
})
export class ContactListComponent implements OnInit {
  contacts: Contact[] = [];
  contactForm: FormGroup;
  searchTerm: string = '';
  isEditing: boolean = false;
  selectedContact: Contact | null = null;

  constructor(
    private contactService: ContactService,
    private fb: FormBuilder
  ) {
    this.contactForm = this.fb.group({
      name: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.contactService.getContacts().subscribe({
      next: (contacts) => this.contacts = contacts,
      error: (error) => console.error('Error loading contacts:', error)
    });
  }

  searchContacts(): void {
    if (this.searchTerm.trim()) {
      this.contactService.searchContacts(this.searchTerm).subscribe({
        next: (contacts) => this.contacts = contacts,
        error: (error) => console.error('Error searching contacts:', error)
      });
    } else {
      this.loadContacts();
    }
  }

  onSubmit(): void {
    if (this.contactForm.valid) {
      const contactData = this.contactForm.value;
      
      if (this.isEditing && this.selectedContact) {
        const updatedContact = { ...this.selectedContact, ...contactData };
        this.contactService.updateContact(updatedContact).subscribe({
          next: () => {
            this.loadContacts();
            this.resetForm();
          },
          error: (error) => console.error('Error updating contact:', error)
        });
      } else {
        this.contactService.addContact(contactData).subscribe({
          next: () => {
            this.loadContacts();
            this.resetForm();
          },
          error: (error) => console.error('Error adding contact:', error)
        });
      }
    }
  }

  editContact(contact: Contact): void {
    this.isEditing = true;
    this.selectedContact = contact;
    this.contactForm.patchValue({
      name: contact.name,
      phoneNumber: contact.phoneNumber,
      email: contact.email
    });
  }

  deleteContact(id: number): void {
    if (confirm('Are you sure you want to delete this contact?')) {
      this.contactService.deleteContact(id).subscribe({
        next: () => this.loadContacts(),
        error: (error) => console.error('Error deleting contact:', error)
      });
    }
  }

  resetForm(): void {
    this.contactForm.reset();
    this.isEditing = false;
    this.selectedContact = null;
  }
}