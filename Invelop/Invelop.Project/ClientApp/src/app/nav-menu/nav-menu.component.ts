import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public readonly items: MenuItem[] = [
    { label: 'Home', icon: 'pi pi-fw pi-home', routerLink: '/' },
    { label: 'Persons Contacts', icon: 'pi pi-fw pi-folder', routerLink: '/persons-contacts' }
  ];
  public activeItem: MenuItem = this.items[0];

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(n => n))
      .subscribe(event => {
        let d: NavigationEnd;
        d = event as NavigationEnd;
        if (d && d.url) {
          const menuItem = this.items.find(m => m.routerLink === d.url);

          if (menuItem) {
            this.activeItem = menuItem;
          }
        }
      });
  }
}
