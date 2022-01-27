import { Component } from '@angular/core';
import { PersonContacts } from '../models/person-contacts.model';
import { PersonContactsService } from '../services/person-contacts.service';

@Component({
  selector: 'app-persons-contacts',
  templateUrl: './persons-contacts.component.html',
  styleUrls: ['./persons-contacts.component.css']
})
export class PersonsContactsComponent {
  public personsContacts: PersonContacts[] = [];
  public dataLoaded: boolean = false;

  constructor(private personContactsService: PersonContactsService) { }

  ngOnInit() {
    this.loadData();
  }

  public Refresh() {
    this.loadData();
  }

  public deletePersonContacts(Id: number) {
    if (confirm('This action will permanently delete the record! Are you sure you want to proceed?')) {
      this.personContactsService.Delete(Id).subscribe(() => {
        this.loadData();
      }, error => console.error(error));
    }
  }

  private loadData() {
    this.dataLoaded = false;
    this.personContactsService.GetAll().subscribe(result => {
      this.personsContacts = result;
      this.dataLoaded = true;
    }, error => console.error(error));
  }
}
