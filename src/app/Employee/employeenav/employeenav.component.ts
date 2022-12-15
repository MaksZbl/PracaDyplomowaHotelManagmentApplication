import { Component, OnInit } from '@angular/core';
import { faDoorOpen, faHome } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employeenav',
  templateUrl: './employeenav.component.html',
  styleUrls: ['../employee/employee.component.css']
})
export class EmployeenavComponent implements OnInit {

  faDoorIcon = faDoorOpen; faHomeIcon = faHome;
  constructor() { }

  ngOnInit(): void {
  }

}
