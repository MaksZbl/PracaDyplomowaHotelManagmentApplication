import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../shared/payment.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-detail',
  templateUrl: './payment-detail.component.html',
  styleUrls: ['./payment-detail.component.css']
})
export class PaymentDetailComponent implements OnInit {

  daysDifference: any;
  price: any;

  constructor(public service: PaymentService, public toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.daysDifference = localStorage.getItem("difDays");
    this.price = localStorage.getItem("price");
  }

  parseToNumber(arg: string) {
    return Number(arg);
  }

  addPayment(form: NgForm) {
    this.service.postPayment().subscribe(
      res => {
        form.form.reset();
        this.toastr.success("Płatność zrealizowana");
        localStorage.removeItem("booking_id");
        this.router.navigate(["/"])

      },
      err => { console.log(err) }
    )
    //console.log(this.service.formData)
  }

}