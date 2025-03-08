import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Contact {
  id: number;
  name: string;
  phoneNumber: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = 'http://localhost:5276/api/contacts';

  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.apiUrl);
  }

  getContact(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`);
  }

  searchContacts(searchTerm: string): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.apiUrl}/search?searchTerm=${searchTerm}`);
  }

  addContact(contact: Omit<Contact, 'id'>): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, contact);
  }

  updateContact(contact: Contact): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${contact.id}`, contact);
  }

  deleteContact(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}