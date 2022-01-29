import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateServiceService {

  constructor() { }

  public toLocalDateFromServerUtcDate(dateToFormat: any) {
    if (dateToFormat) {
      var date = new Date(dateToFormat);
      return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours());
    }

    return undefined;
  }

  public toUtcDateFromLocalDate(date: Date) {
    if (date) {
      return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours()));
    }

    return undefined;
  }
}
