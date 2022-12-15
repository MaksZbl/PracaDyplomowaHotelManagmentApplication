import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { JwtHelperService } from '@auth0/angular-jwt';
import { faDoorOpen, faHome, faCheck, faExclamation } from '@fortawesome/free-solid-svg-icons';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employeepanelrooms',
  templateUrl: './employeepanelrooms.component.html',
  styleUrls: ['../employee.component.css']
})
export class EmployeepanelroomsComponent implements OnInit {
  userName: string = sessionStorage.getItem("username") || "";
  user: any;
  userHotel: string;
  roomsList: any;
  faDoorIcon = faDoorOpen; faHomeIcon = faHome; faCheckIcon = faCheck; faExclamationIcon = faExclamation;
  constructor(private http: HttpClient, public toastr: ToastrService) { }
  readonly baseUrlUsers = `https://localhost:5001/api/LoggedInUsers`;
  readonly baseUrlRooms = `https://localhost:5001/api/Rooms`;
  jtwHelper = new JwtHelperService();

  getRooms() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${sessionStorage.getItem("jwt") || ""}`,
      })
    };

    this.http.get(`${this.baseUrlUsers}/${this.userName}`, httpOptions).subscribe(response => {
      this.user = response;
      this.userHotel = this.user.hotel.title;
      this.roomsList = this.user.hotel.rooms;
      console.log(this.userHotel);
      console.log(this.roomsList);
    });
  }

  EditRoomIsFree(id: number, desc: string, number: string, type: string, isFree: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${sessionStorage.getItem("jwt") || ""}`,
      })
    };

    const credentials = {
      'isFree': !isFree,
      'number': number,
      'description': desc,
      'type': type,
    }

    this.http.put(`${this.baseUrlRooms}/${id}`, credentials, httpOptions).subscribe(
      res => {
        this.getRooms();
        this.toastr.success("Dostępność zmieniona", "Odpowiedz aplikacji");
      },
      err => { console.log(err) }
    );;
  }

  ngOnInit(): void {
    this.getRooms();
  }
}
