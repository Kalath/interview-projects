import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PersonContacts } from '../models/person-contacts.model';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public personsContacts: PersonContacts[] = [];
  private readonly httpClient: HttpClient;
  private readonly baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;

    this.loadData();
  }

  public Refresh() {
    this.loadData();
  }

  public deletePersonContacts(id: number) {
    this.httpClient.delete<PersonContacts[]>(this.baseUrl + 'personcontacts/' + id).subscribe(result => {
      this.loadData();
    }, error => console.error(error));
  }

  private loadData() {
    this.httpClient.get<PersonContacts[]>(this.baseUrl + 'personcontacts').subscribe(result => {
      this.personsContacts = result;
    }, error => console.error(error));
  }
}
