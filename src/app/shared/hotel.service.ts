import { Injectable } from '@angular/core';
import { Hotel } from './hotel.model'
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class HotelService {

  Hotel: Hotel = new Hotel();
  constructor(private http: HttpClient) { }
  readonly baseUrl = '/api/Hotels';

  getHotels() {
    return this.http.get(this.baseUrl);
  }
}
