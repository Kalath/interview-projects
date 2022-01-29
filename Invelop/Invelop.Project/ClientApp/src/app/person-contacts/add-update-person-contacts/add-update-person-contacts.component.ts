import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize, map, takeUntil } from 'rxjs/operators';
import { PersonContacts } from '../shared/models/person-contacts.model';
import { PersonContactsService } from '../shared/services/person-contacts.service';
import { NgForm } from '@angular/forms';
import { DateServiceService } from 'src/app/shared/services/date-service.service';

@Component({
  selector: 'app-add-update-person-contacts',
  templateUrl: './add-update-person-contacts.component.html',
  styleUrls: ['./add-update-person-contacts.component.css']
})
export class AddUpdatePersonContactsComponent implements OnInit {
  public readonly maxCalendarDate = new Date();
  private readonly ngUnsubscribe = new Subject();

  public inProgess: boolean = false;
  public editMode: boolean = false;
  public personContacts: PersonContacts = {} as PersonContacts;

  constructor(
    private personContactsService: PersonContactsService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dateServiceService: DateServiceService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      let Id = Number(params['Id']);

      if (Id) {
        this.inProgess = true;
        this.personContactsService.Get(Id)
          .pipe(finalize(() => {
            this.inProgess = false;
          }),
            map(p => {
              p.dateOfBirth = this.dateServiceService.toLocalDateFromServerUtcDate(p.dateOfBirth) as any
              return p;
            }),
            takeUntil(this.ngUnsubscribe))
          .subscribe(result => {
            this.personContacts = result;
            this.editMode = true;
          });
      }
    });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public addPersonContacts(form: NgForm) {
    if (form.valid) {
      this.inProgess = true;
      const modelToAdd = this.formatModel(this.personContacts);

      this.personContactsService.Insert(modelToAdd)
        .pipe(finalize(() => {
          this.inProgess = false;
        }),
          takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          alert('Added! Returning to Persons Contacts.');
          this.navigateToMain();
        }, error => {
          alert('Failed! Check console.');
          console.log(error);
        });
    }
  }

  public updatePersonContacts(form: NgForm) {
    if (form.valid) {
      this.inProgess = true;
      const modelToUpdate = this.formatModel(this.personContacts);

      this.personContactsService.Update(modelToUpdate)
        .pipe(finalize(() => {
          this.inProgess = false;
        }),
          takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          alert('Updated! Returning to Persons Contacts.');
          this.navigateToMain();
        }, error => {
          alert('Failed! Check console.');
          console.log(error);
        });
    }
  }

  private navigateToMain() {
    this.router.navigate(['/persons-contacts'])
  }

  private formatModel(modelToFormat: PersonContacts) {
    return {
      id: modelToFormat.id,
      firstname: modelToFormat.firstname,
      surname: modelToFormat.surname,
      dateOfBirth: this.dateServiceService.toUtcDateFromLocalDate(modelToFormat.dateOfBirth),
      address: modelToFormat.address,
      iban: modelToFormat.iban,
      phoneNumber: modelToFormat.phoneNumber
    } as PersonContacts
  }
}
