import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  constructor(private http: HttpClient) { }

  currentHotelId: number = 0;


  getAvRate(hotelId: number) {
    this.currentHotelId = hotelId;
    var baseUrl = `https://localhost:5001/api/Rates/${this.currentHotelId}`;
    return this.http.get(baseUrl);
  }
}
