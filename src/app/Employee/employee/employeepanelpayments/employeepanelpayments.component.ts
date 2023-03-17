import { Component, OnInit } from '@angular/core';
import { PaymentService } from 'src/app/shared/payment.service';
import { faDoorOpen, faHome, faCheck, faExclamation, faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employeepanelpayments',
  templateUrl: './employeepanelpayments.component.html',
  styleUrls: ['../employee.component.css']
})
export class EmployeepanelpaymentsComponent implements OnInit {

  constructor(private service: PaymentService) { }
  PaymentList: any;
  faDoorIcon = faDoorOpen; faHomeIcon = faHome; faCheckIcon = faCheck; faExclamationIcon = faExclamation; faUserIcon = faUser;
  user: any = sessionStorage.getItem("role");
  nameOfUser: any = sessionStorage.getItem("firstname");
  surnameOfUser: any = sessionStorage.getItem("lastname");
  roleValue: any = sessionStorage.getItem("role");
  ngOnInit(): void {
    this.service.getPayments().subscribe(response => {
      this.PaymentList = response;
      console.log(this.PaymentList);
    })
  }

}
