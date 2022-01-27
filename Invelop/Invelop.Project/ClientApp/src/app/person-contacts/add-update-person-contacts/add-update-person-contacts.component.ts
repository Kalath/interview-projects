import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonContacts } from '../models/person-contacts.model';
import { PersonContactsService } from '../services/person-contacts.service';

@Component({
  selector: 'app-add-update-person-contacts',
  templateUrl: './add-update-person-contacts.component.html',
  styleUrls: ['./add-update-person-contacts.component.css']
})
export class AddUpdatePersonContactsComponent implements OnInit {
  public editMode: boolean = false;
  public personContacts: PersonContacts = {} as PersonContacts;

  constructor(private personContactsService: PersonContactsService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      var Id = Number(params['Id']);

      if (Id) {
        this.personContactsService.Get(Id).subscribe(result => {
          this.personContacts = result;
          this.editMode = true;
          console.log(this.personContacts);
        }, error => console.error(error));
      }
    });
  }

  public addPersonContacts() {
    this.personContactsService.Insert(this.personContacts).subscribe(() => {
      alert('Added! Returning to Persons Contacts.');
      this.navigateToMain();
    }, error => {
      alert('Failed! Check console.');
      console.log(error);
    });
  }

  public updatePersonContacts() {
    this.personContactsService.Update(this.personContacts).subscribe(() => {
      alert('Updated! Returning to Persons Contacts.');
      this.navigateToMain();
    }, error => {
      alert('Failed! Check console.');
      console.log(error);
    });

  }

  private navigateToMain() {
    this.router.navigate(['/persons-contacts'])
  }
}
