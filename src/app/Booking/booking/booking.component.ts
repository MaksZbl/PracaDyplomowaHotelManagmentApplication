import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/shared/booking.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  constructor(private service: BookingService) { }

  ngOnInit(): void {
    this.service.checkCurrentHotel();
  }

}
