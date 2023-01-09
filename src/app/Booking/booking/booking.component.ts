import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/shared/booking.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Booking } from 'src/app/shared/booking.model';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  readonly baseUrlHotel = `https://localhost:5001/api/Hotels`;

  constructor(private service: BookingService, private router: Router, private http: HttpClient) { }

  booking: Booking = new Booking();

  myControl = new FormControl('');
  options: string[] = ['2-person', 'Family 5-person', 'Apartments 4-person'];
  filteredOptions: Observable<string[]>;
  invalidDateOrType: boolean = false;

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
      this.booking.endDate = this.range.controls.end.value || new Date();
      this.booking.description = `  ${this.hotel[0].hotel_id}  `
      this.service.postBooking(this.booking);
    });
  }


  ngOnInit(): void {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this.filterAutocomplete(value || '')),
    );
  }

}
