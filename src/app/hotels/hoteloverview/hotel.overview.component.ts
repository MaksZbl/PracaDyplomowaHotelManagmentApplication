import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http'
import { faSnowflake, faWineGlass, faUtensils, faParking, faDumbbell, faSwimmingPool, faTableTennis, faSpa, faHotTub, faWifi, faChild, faFlag, faStar, faCamera, faEnvelope } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-hotel-piotrkow',
  templateUrl: './hotel.overview.component.html',
  styleUrls: ['./hotel.overview.component.css']
})
export class HotelOverviewComponent implements OnInit {

  constructor(private router: Router, private http: HttpClient) { }
  faSnowflakeIcon = faSnowflake; faWineGlassIcon = faWineGlass; faUtensilsIcon = faUtensils; faParkingIcon = faParking; faDumbbellIcon = faDumbbell; faSwimmingPoolIcon = faSwimmingPool; faTableTennisIcon = faTableTennis; faSpaIcon = faSpa; faHotTubIcon = faHotTub; faWifiIcon = faWifi; faChildIcon = faChild; faFlagIcon = faFlag; faStarIcon = faStar; faCameraIcon = faCamera; faEnvelopeIcon = faEnvelope;
  hotel: any;
  images: any[] = [];
  roomsImages: any;
  viewAll: boolean = false;
  type: number;
  buttonServicesText: string = "Zobacz wszystkie usługi";
  comfortableServices: boolean = false; luxuryServices: boolean = false; spaServices: boolean = false;
  readonly baseUrlHotel = `https://localhost:5001/api/Hotels`;

  CheckHotel() {
    var url = this.router.url;
    var str = "";
    for (let i = url.length - 15; i < url.length; i++) {
      if (i + 1 < url.length) {
        if (url[i + 1] === url[i + 1].toUpperCase()) {
          str += url[i] + " ";
        }
        else {
          str += url[i];
        }
      }
      if (i == url.length) {
        break;
      }
    }

    return str;
  }

  checkServices() {
    if (this.hotel[0].title.toLowerCase().includes("luxury")) {
      this.luxuryServices = true;
      console.log(this.luxuryServices);
    }
    if (this.hotel[0].title.toLowerCase().includes("comfortable")) {
      this.comfortableServices = true;
      console.log(this.comfortableServices);
    }
    if (this.hotel[0].title.toLowerCase().includes("spa")) {
      this.spaServices = true;
      console.log(this.spaServices);
    }
  }

  checkType() {
    if (this.hotel[0].type.toLowerCase() === "5-stars") {
      this.type = 5;
      console.log(this.type);
    }
    if (this.hotel[0].type.toLowerCase() === "4-stars") {
      this.type = 4;
      console.log(this.type);
    }
  }

  SeeAll() {
    if (this.viewAll == false) {
      this.viewAll = true;
      this.buttonServicesText = "Zobacz mniej usług";
    }
    else {
      this.viewAll = false;
      this.buttonServicesText = "Zobacz wszystkie usługi";
    }
    console.log(this.viewAll);
  }

  ngOnInit(): void {
    const str = this.CheckHotel();
    this.http.get(`${this.baseUrlHotel}/${str}`).subscribe(response => {
      this.hotel = response;
      this.images = this.hotel[0].images;
      this.roomsImages = this.hotel[0].rooms[0].images;
      console.log(this.roomsImages)
      this.checkServices();
      this.checkType();
    });
  }
}
