import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { PaymentService } from '../shared/payment.service';

@Component({
  selector: 'app-my-payments',
  templateUrl: './my-payments.component.html',
  styleUrls: ['./my-payments.component.css']
})
export class MyPaymentsComponent implements OnInit {

  public jwtHelper = new JwtHelperService();

  paymentsList: any;

  constructor(public toastr: ToastrService, private router: Router, public service: PaymentService) { }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwt") == null || this.jwtHelper.isTokenExpired(sessionStorage.getItem("jwt") || "")) {
      setTimeout(() => {
        this.toastr.error("Zaloguj sie");
        this.router.navigate(["/"]);
      }, 1)
    }

    this.service.getPayments().subscribe(response => {
      this.paymentsList = response;
      console.log(this.paymentsList)
    });
  }
}
