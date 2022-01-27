import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdatePersonContactsComponent } from './add-update-person-contacts.component';

//Too lazy to add unit tests, but they are a MUST do
describe('AddUpdatePersonContactsComponent', () => {
  let component: AddUpdatePersonContactsComponent;
  let fixture: ComponentFixture<AddUpdatePersonContactsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUpdatePersonContactsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUpdatePersonContactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
