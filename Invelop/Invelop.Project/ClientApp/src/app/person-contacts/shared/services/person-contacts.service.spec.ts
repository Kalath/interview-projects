import { TestBed } from '@angular/core/testing';

import { PersonContactsService } from './person-contacts.service';

//Too lazy to add unit tests, but they are a MUST do
describe('PersonContactsService', () => {
  let service: PersonContactsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PersonContactsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
