import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Booking } from './booking.model';
import { ToastrService } from 'ngx-toastr';
import { HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private router: Router, private http: HttpClient, public toastr: ToastrService) { }

  readonly baseUrl = '/api/Bookings';


  checkDate(date: Date) {
    console.log(typeof (date));
  }

  postBooking(booking: Booking) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${sessionStorage.getItem("jwt") || ""}`,
      })
    };
    const credentials = {
      'startDate': booking.startDate,
      'endDate': booking.endDate,
      'type': booking.type,
      'title': booking.title,
      'description': booking.description,
    }

    this.http.post<any>(this.baseUrl, credentials, httpOptions)
      .subscribe(response => {
        this.toastr.success("Rezerwacja zakonczona, przejdż do swoich rezerwacji przy pomocy panelu użytkownika");
      }, err => {
        this.toastr.error("Błąd rezerwacji, wystąpił problem")
        console.log(err);
      })
  }
}
