import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { HotelService } from '../../shared/hotel.service';
import { faBook, faTag, faMoneyCheckAlt, faArrowCircleRight, faToolbox, faEye } from '@fortawesome/free-solid-svg-icons';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  AuthorizeUserRole: string; AuthorizeUserFirstname: string; AuthorizeUserLastname: string; AuthorizeUserUsername: string
  isDisplay: boolean = false;
  isDisplayCabinet: boolean = false;
  invalidLogin: boolean;
  AuthorizeUser: boolean = false;
  username: string;
  password: string;
  type: string = "password";
  faBookIcon = faBook; faMoneyIcon = faMoneyCheckAlt; faTagIcon = faTag; faArrowCircleRightIcon = faArrowCircleRight; faToolboxIcon = faToolbox; faEyeIcon = faEye;

  constructor(private router: Router, private http: HttpClient, public service: HotelService, public toastr: ToastrService) {
  }

  ShowPassword() {
    if (this.type == "password") {
      this.type = "text";
    }
    else {
      this.type = "password";
    }
  }
  login(form: NgForm) {
    const credentials = {
      'userName': this.username,
      'password': this.password,
    }

    this.http.post<any>("/api/Login", credentials)
      .subscribe(response => {
        this.invalidLogin = false;
        const token = (<any>response).token;
        const decodedToken = this.jwtHelper.decodeToken(token)
        console.log(decodedToken)
        const str = JSON.stringify(decodedToken);
        console.log(str)
        // role
        var newStr = str.split('role":"')[1];
        newStr = newStr.split('",')[0]
        sessionStorage.setItem("jwt", token);
        this.AuthorizeUserRole = newStr;
        sessionStorage.setItem("role", this.AuthorizeUserRole);

        //FirstName
        var newStr = str.split('givenname":"')[1];
        newStr = newStr.split('",')[0]
        sessionStorage.setItem("jwt", token);
        this.AuthorizeUserFirstname = newStr;
        sessionStorage.setItem("firstname", this.AuthorizeUserFirstname);

        //LastName
        var newStr = str.split('surname":"')[1];
        newStr = newStr.split('",')[0]
        sessionStorage.setItem("jwt", token);
        this.AuthorizeUserLastname = newStr;
        sessionStorage.setItem("lastname", this.AuthorizeUserLastname);

        //UserName
        var newStr = str.split('nameidentifier":"')[1];
        newStr = newStr.split('",')[0]
        sessionStorage.setItem("jwt", token);
        this.AuthorizeUserUsername = newStr;
        sessionStorage.setItem("username", this.AuthorizeUserUsername);
        this.isDisplay = !this.isDisplay;
        this.AuthorizeUser = true;
        this.router.navigate(["/"]);
        this.toastr.success("Udane zalogowanie", "Logowanie");
        this.router.navigate([this.router.url]);

      }, err => {
        console.log(err)
        this.invalidLogin = true;
      })
  }
  DisplayOverlay() {
    this.isDisplay = !this.isDisplay;
    console.log(this.isDisplay);
  }

  DisplayCabinetOverlay() {
    this.isDisplayCabinet = !this.isDisplayCabinet;
  }

  LogOut() {
    sessionStorage.removeItem("jwt");
    sessionStorage.removeItem("role");
    sessionStorage.removeItem("username");
    sessionStorage.removeItem("firstname");
    sessionStorage.removeItem("lastname");
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwt") === null) {
      this.LogOut();
      this.AuthorizeUser = false;
    }
    else {
      this.AuthorizeUserFirstname = sessionStorage.getItem("firstname") || "";
      this.AuthorizeUserLastname = sessionStorage.getItem("lastname") || "";
      this.AuthorizeUser = true;
    }
  }

}
