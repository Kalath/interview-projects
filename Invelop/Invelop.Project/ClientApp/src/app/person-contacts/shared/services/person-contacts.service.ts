import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonContacts } from '../models/person-contacts.model';

@Injectable({
  providedIn: 'root'
})
export class PersonContactsService {
  private readonly baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public GetAll(): Observable<PersonContacts[]> {
    return this.http.get<PersonContacts[]>(this.baseUrl + 'personcontacts');
  }

  public Delete(Id: number): Observable<any> {
    return this.http.delete(this.baseUrl + 'personcontacts/' + Id);
  }

  public Get(Id: number): Observable<PersonContacts> {
    return this.http.get<PersonContacts>(this.baseUrl + 'personcontacts/' + Id);
  }

  public Insert(personContacts: PersonContacts): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'personcontacts', personContacts);
  }

  public Update(personContacts: PersonContacts): Observable<any> {
    return this.http.put<any>(this.baseUrl + 'personcontacts', personContacts);
  }  
}
