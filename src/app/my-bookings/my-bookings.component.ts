import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {

  constructor(public router: Router, public toastr: ToastrService) { }

  public jwtHelper: JwtHelperService = new JwtHelperService();
  ngOnInit(): void {
    if (sessionStorage.getItem("jwt") == null || this.jwtHelper.isTokenExpired(sessionStorage.getItem("jwt") || "")) {
      setTimeout(() => {
        this.toastr.error("Zaloguj sie");
        this.router.navigate(["/"]);
      }, 1)
    }

  }

}
