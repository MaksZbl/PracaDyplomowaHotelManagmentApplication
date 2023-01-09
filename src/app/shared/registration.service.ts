import { Injectable } from '@angular/core';
import { Registration } from './registration.model';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  readonly baseUrl = "/api/Registration";
  constructor(public http: HttpClient, public toastr: ToastrService) { }

  postRegistration(registration: Registration) {
    const credentials = {
      'firstName': registration.firstName,
      'lastName': registration.lastName,
      'userName': registration.userName,
      'password': registration.password,
      'emailAdress': registration.emailAdress,
      'registrationDate': registration.registrationDate,
      'adress': registration.address,
      'mobile': registration.mobile,
    }

    this.http.post<any>(this.baseUrl, credentials)
      .subscribe(response => {
        this.toastr.success("Rejestracja zakonczona")
      }, err => {
        this.toastr.error("Błąd rejestracji, taki użytkownik już jest zarejestrowany")
        console.log(err);
      })
  }
}
