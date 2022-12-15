import { Component, OnInit } from '@angular/core';
import { Hotel } from '../../shared/hotel.model';
import { HotelService } from '../../shared/hotel.service';
import { DomSanitizer } from '@angular/platform-browser'

@Component({
  selector: 'app-hotel-content',
  templateUrl: './hotel-content.component.html',
  styleUrls: ['./hotel-content.component.css']
})
export class HotelContentComponent implements OnInit {

  HotelList: any;
  HotelListRoutes: string[] = [];
  images: any;

  constructor(public service: HotelService, private _sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.service.getHotels().subscribe(response => {
      this.HotelList = response;
      console.log(this.HotelList)
      for (let index = 0; index < this.HotelList.length; index++) {
        this.HotelListRoutes.push(this.HotelList[index].title);
        this.HotelListRoutes[index] = this.HotelListRoutes[index].replaceAll(" ", "");
        this.HotelListRoutes[index] = this.HotelListRoutes[index].replaceAll("ł", "l");
        this.HotelListRoutes[index] = this.HotelListRoutes[index].replaceAll("ń", "n");
      }
      console.log(this.HotelListRoutes)
    });
  }
}

