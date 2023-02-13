import { Injectable } from '@angular/core';
import { PaymentDetail } from './payment.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  formData: PaymentDetail = new PaymentDetail();
  readonly baseUrl = "/api/Payments";
  constructor(private http: HttpClient) { }



  postPayment() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${sessionStorage.getItem("jwt") || ""}`,
      })
    };
    this.formData.booking_id = Number(localStorage.getItem("booking_id"));
    return this.http.post(this.baseUrl, this.formData, httpOptions);
  }
}
