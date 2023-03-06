import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Rate } from './rate.model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  constructor(private http: HttpClient, public toastr: ToastrService) { }

  currentHotelId: number = 0;


  getAvRate(hotelId: number) {
    this.currentHotelId = hotelId;
    var baseUrl = `https://localhost:5001/api/Rates/${this.currentHotelId}`;
    return this.http.get(baseUrl);
  }

  postRate(rate: Rate, username: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${sessionStorage.getItem("jwt") || ""}`,
      })
    };

    const credentials = {
      'rateId': 0,
      'value': rate.value,
      'loggedInUserId': rate.loggedInUserId,
      'hotelId': rate.hotelId,
    }

    var baseUrl = `https://localhost:5001/api/Rates`;

    this.http.post<any>(baseUrl, credentials, httpOptions)
      .subscribe(response => {
        this.toastr.success("Dziękujemy za twoją ocenę");
      }, err => {
        this.toastr.error("Zaloguj się żeby dodać opinie")
        console.log(err);
      })
  }
}
