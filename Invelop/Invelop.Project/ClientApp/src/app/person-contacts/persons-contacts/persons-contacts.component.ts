import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { DateServiceService } from 'src/app/services/date-service.service';
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
  private readonly ngUnsubscribe = new Subject();

  constructor(private personContactsService: PersonContactsService, private dateServiceService: DateServiceService) { }

  ngOnInit() {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public Refresh() {
    this.loadData();
  }

  public deletePersonContacts(Id: number) {
    if (confirm('This action will permanently delete the record! Are you sure you want to proceed?')) {
      this.personContactsService.Delete(Id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.loadData();
        }, error => console.error(error));
    }
  }

  private loadData() {
    this.dataLoaded = false;
    this.personContactsService.GetAll()
      .pipe(map(p => {
        if (p && p.length > 0) {
          p.forEach(item => {
            item.dateOfBirth = this.dateServiceService.toLocalDateFromServerUtcDate(item.dateOfBirth) as any;
          })
        }
        return p;
      }),
        takeUntil(this.ngUnsubscribe))
      .subscribe(result => {
        this.personsContacts = result;
        this.dataLoaded = true;
      }, error => console.error(error));
  }
}
