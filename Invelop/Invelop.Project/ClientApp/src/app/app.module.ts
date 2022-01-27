import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PersonsContactsComponent } from './person-contacts/persons-contacts/persons-contacts.component';
import { AddUpdatePersonContactsComponent } from './person-contacts/add-update-person-contacts/add-update-person-contacts.component';
import { CalendarModule } from 'primeng/calendar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { KeyFilterModule } from 'primeng/keyfilter';
import { MessageModule } from 'primeng/message';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PersonsContactsComponent,
    AddUpdatePersonContactsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CalendarModule,
    BrowserAnimationsModule,
    TableModule,
    InputTextModule,
    KeyFilterModule,
    MessageModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'persons-contacts', component: PersonsContactsComponent },
      { path: 'add-update-person-contacts', component: AddUpdatePersonContactsComponent },
      { path: 'add-update-person-contacts/:Id', component: AddUpdatePersonContactsComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
