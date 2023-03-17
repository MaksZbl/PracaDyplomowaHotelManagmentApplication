import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { faSnowflake, faWineGlass, faUtensils, faParking, faDumbbell, faSwimmingPool, faTableTennis, faSpa, faHotTub, faWifi, faChild, faFlag, faStar, faCamera, faEnvelope } from '@fortawesome/free-solid-svg-icons';
import { BookingComponent } from 'src/app/Booking/booking/booking.component';
import { HeaderComponent } from 'src/app/home/header/header.component';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { RateService } from 'src/app/shared/rate.service';
import { Rate } from 'src/app/shared/rate.model';

@Component({
  selector: 'app-hotel-piotrkow',
  templateUrl: './hotel.overview.component.html',
  styleUrls: ['./hotel.overview.component.css']
})
export class HotelOverviewComponent implements OnInit {

  constructor(private router: Router, private http: HttpClient, public dialog: MatDialog, public toastr: ToastrService, public rateService: RateService) { }
  faSnowflakeIcon = faSnowflake; faWineGlassIcon = faWineGlass; faUtensilsIcon = faUtensils; faParkingIcon = faParking; faDumbbellIcon = faDumbbell; faSwimmingPoolIcon = faSwimmingPool; faTableTennisIcon = faTableTennis; faSpaIcon = faSpa; faHotTubIcon = faHotTub; faWifiIcon = faWifi; faChildIcon = faChild; faFlagIcon = faFlag; faStarIcon = faStar; faCameraIcon = faCamera; faEnvelopeIcon = faEnvelope;
  hotel: any;
  images: any[] = [];
  roomsImages: any;
  viewAll: boolean = false;
  currentRate: number = 0;
  rateOfHotel: any;
  type: number;
  countofReviews: number = 0;
  buttonServicesText: string = "Zobacz wszystkie usługi";
  comfortableServices: boolean = false; luxuryServices: boolean = false; spaServices: boolean = false;
  jwtHelper: JwtHelperService = new JwtHelperService();
  rate: Rate = new Rate();
  readonly baseUrlHotel = `https://localhost:5001/api/Hotels`;

  CheckHotel() {
    var url = this.router.url;
    var str = "";
    for (let i = 10; i < url.length; i++) {
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

  openBookingDialog() {
    if (sessionStorage.getItem("jwt") == null || this.jwtHelper.isTokenExpired(sessionStorage.getItem("jwt") || "")) {
      this.toastr.error("Zaloguj sie żeby kontynuować");
      return null;
    }
    const dialogRef = this.dialog.open(BookingComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      return result;
    });
    return null;
  }

  clickRate() {
    setTimeout(
      () => {
        this.rateOfHotel = this.rateService.getAvRate(this.hotel[0].hotel_id).subscribe((res: any) => {
          this.currentRate = res.avRate;
          this.countofReviews = res.count;
        });
      }, 1000
    )
    this.rate.value = this.currentRate;
    this.rate.hotelId = this.hotel[0].hotel_id;
    console.log(this.rate.hotelId);
    this.rate.value = this.currentRate;
    console.log(this.rate.value);
    this.rateService.postRate(this.rate, sessionStorage.getItem("username") || "");

  }

  ngOnInit(): void {
    const str = this.CheckHotel();
    console.log(str);
    this.http.get(`${this.baseUrlHotel}/${str}`).subscribe(response => {
      console.log(str);
      this.hotel = response;
      this.images = this.hotel[0].images;
      this.roomsImages = this.hotel[0].rooms[0].images;
      console.log(this.hotel)
      this.checkServices();
      this.checkType();
      this.rateOfHotel = this.rateService.getAvRate(this.hotel[0].hotel_id).subscribe((res: any) => {
        this.currentRate = res.avRate;
        this.currentRate = Number(this.currentRate.toFixed(1));
        this.countofReviews = res.count;
      });
    });
  }
}
