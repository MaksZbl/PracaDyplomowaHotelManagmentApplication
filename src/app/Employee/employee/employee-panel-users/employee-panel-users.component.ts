import { Component, OnInit } from '@angular/core';
import { faDoorOpen, faHome, faCheck, faExclamation, faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employee-panel-users',
  templateUrl: './employee-panel-users.component.html',
  styleUrls: ['../employee.component.css']
})
export class EmployeePanelUsersComponent implements OnInit {

  constructor() { }

  faDoorIcon = faDoorOpen; faHomeIcon = faHome; faUserIcon = faUser;
  user: any = sessionStorage.getItem("role");
  nameOfUser: any = sessionStorage.getItem("firstname");
  surnameOfUser: any = sessionStorage.getItem("lastname");
  roleValue: any = sessionStorage.getItem("role");

  ngOnInit(): void {
  }

}
