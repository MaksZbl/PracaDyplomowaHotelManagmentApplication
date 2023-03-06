import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/shared/booking.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Booking } from 'src/app/shared/booking.model';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { faBed } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  readonly baseUrlHotel = `https://localhost:5001/api/Hotels`;

  constructor(private service: BookingService, private router: Router, private http: HttpClient, public dialog: MatDialog) { }

  booking: Booking = new Booking();

  myControl = new FormControl('');
  options: string[] = ['2-person', 'Family 5-person', 'Apartments 4-person'];
  filteredOptions: Observable<string[]>;
  invalidDateOrType: boolean = false;
  faBedIcon = faBed;
  countOfTwoPerson: number = 0;
  countOfFourPerson: number = 0;
  countOfFivePerson: number = 0;

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  hotel: any;



  checkCurrentHotel() {
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

  myFilter = (d: Date | null): boolean => {
    const year = (d || new Date()).getFullYear();
    const currentDate = (d || new Date()).getDate();
    return year == 2023 && new Date().getDate() < currentDate;
  };

  private filterAutocomplete(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  submitBookingForm() {
    var str = this.checkCurrentHotel();
    this.http.get(`${this.baseUrlHotel}/${str}`).subscribe(response => {
      this.hotel = response;
      this.booking.startDate = this.range.controls.start.value || new Date();
      this.booking.startDate = new Date(Date.UTC(this.booking.startDate.getFullYear(), this.booking.startDate.getMonth(), this.booking.startDate.getDate()));
      this.booking.endDate = this.range.controls.end.value || new Date();
      this.booking.endDate = new Date(Date.UTC(this.booking.endDate.getFullYear(), this.booking.endDate.getMonth(), this.booking.endDate.getDate()));
      this.booking.description = `  ${this.hotel[0].hotel_id}  `
      this.service.postBooking(this.booking);
    });
    var timeDif = Math.abs(Number((this.range.controls.end.value)?.getTime()) - Number((this.range.controls.start.value)?.getTime()));
    var diffDays = Math.ceil(timeDif / (1000 * 3600 * 24));
    this.dialog.closeAll();
    this.router.navigate(["myBookings"]);
  }


  ngOnInit(): void {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this.filterAutocomplete(value || '')),
    );
    var str = this.checkCurrentHotel();
    this.http.get(`${this.baseUrlHotel}/${str}`).subscribe(response => {
      this.hotel = response;
      console.log(this.hotel)
      for (let index = 0; index < this.hotel[0].rooms.length; index++) {
        if (this.hotel[0].rooms[index].type == "2-person" && this.hotel[0].rooms[index].isFree === true) {
          this.countOfTwoPerson++;
        }
        if (this.hotel[0].rooms[index].type == "Apartments 4-person" && this.hotel[0].rooms[index].isFree === true) {
          this.countOfFourPerson++;
        }
        if (this.hotel[0].rooms[index].type == "Family 5-person" && this.hotel[0].rooms[index].isFree === true) {
          this.countOfFivePerson++;
        }
      }
    });
  }
}
