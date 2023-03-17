import { Component, OnInit } from '@angular/core';
import { faDoorOpen, faHome, faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employeenav',
  templateUrl: './employeenav.component.html',
  styleUrls: ['../employee/employee.component.css']
})
export class EmployeenavComponent implements OnInit {

  faDoorIcon = faDoorOpen; faHomeIcon = faHome; faUserIcon = faUser;
  user: any = sessionStorage.getItem("role");
  nameOfUser: any = sessionStorage.getItem("firstname");
  surnameOfUser: any = sessionStorage.getItem("lastname");
  roleValue: any = sessionStorage.getItem("role");
  constructor() { }

  ngOnInit(): void {
  }

}
