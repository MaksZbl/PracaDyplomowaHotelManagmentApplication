import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Booking } from '../shared/booking.model';
import { BookingService } from '../shared/booking.service';
import * as moment from 'moment';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {

  constructor(public router: Router, public toastr: ToastrService, public service: BookingService) { }

  bookingsList: any;
  roomsList: any;
  public jwtHelper: JwtHelperService = new JwtHelperService();

  toMoment(arg: Date) {
    return moment(arg).format("LL");
  }

  setBookingId(id: number) {
    localStorage.setItem("booking_id", id.toString());
  }


  ngOnInit(): void {
    if (sessionStorage.getItem("jwt") == null || this.jwtHelper.isTokenExpired(sessionStorage.getItem("jwt") || "")) {
      setTimeout(() => {
        this.toastr.error("Zaloguj sie");
        this.router.navigate(["/"]);
      }, 1)
    }

    this.service.getBookings().subscribe(response => {
      this.bookingsList = response;
      console.log(this.bookingsList)
    });
  }

}
