import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PersonsContactsComponent } from './persons-contacts.component';

//Too lazy to add unit tests, but they are a MUST do
describe('PersonsContactsComponent', () => {
  let component: PersonsContactsComponent;
  let fixture: ComponentFixture<PersonsContactsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PersonsContactsComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonsContactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
