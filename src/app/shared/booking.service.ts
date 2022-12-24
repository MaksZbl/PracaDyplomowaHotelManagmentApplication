import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private router: Router, private http: HttpClient) { }

  checkCurrentHotel() {
    var url = this.router.url;
    console.log(url);
  }
}
