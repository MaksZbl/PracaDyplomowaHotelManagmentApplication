import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingComponent } from './Booking/booking/booking.component';
import { AuthPanelGuard } from './Employee/Auth/auth-panel.guard';
import { EmployeeComponent } from './Employee/employee/employee.component';
import { EmployeepanelroomsComponent } from './Employee/employee/employeepanelrooms/employeepanelrooms.component';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './home/registration/registration.component';
import { HotelOverviewComponent } from './hotels/hoteloverview/hotel.overview.component';
import { HotelsComponent } from './hotels/hotels.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'hotels', component: HotelsComponent },
  { path: 'hotels/MemoriesPalaceHotels&SpaPiotrkowTrybunalski', component: HotelOverviewComponent },
  { path: 'hotels/MemoriesPalaceLuxuryResortZakopane', component: HotelOverviewComponent },
  { path: 'hotels/MemoriesPalaceLuxuryResortWarszawa', component: HotelOverviewComponent },
  { path: 'hotels/MemoriesPalaceComfortableResortGdansk', component: HotelOverviewComponent },
  { path: 'hotels/MemoriesPalaceLuxuryResort&SpaWroclaw', component: HotelOverviewComponent },
  { path: 'hotels/MemoriesPalaceLuxuryResort&SpaGdynia', component: HotelOverviewComponent },
  { path: 'employeepanel', component: EmployeeComponent, canActivate: [AuthPanelGuard] },
  { path: 'employeepanel/rooms', component: EmployeepanelroomsComponent, canActivate: [AuthPanelGuard] },
  { path: 'hotels/MemoriesPalaceHotels&SpaPiotrkowTrybunalski/booking', component: BookingComponent, canActivate: [AuthPanelGuard] },
  { path: 'hotels/MemoriesPalaceLuxuryResortZakopane/booking', component: BookingComponent, canActivate: [AuthPanelGuard] },
  { path: 'hotels/MemoriesPalaceLuxuryResortWarszawa/booking', component: BookingComponent, canActivate: [AuthPanelGuard] },
  { path: 'hotels/MemoriesPalaceComfortableResortGdansk/booking', component: BookingComponent, canActivate: [AuthPanelGuard] },
  { path: 'hotels/MemoriesPalaceLuxuryResort&SpaWroclaw/booking', component: BookingComponent, canActivate: [AuthPanelGuard] },
  { path: 'hotels/MemoriesPalaceLuxuryResort&SpaGdynia/booking', component: BookingComponent, canActivate: [AuthPanelGuard] },
  { path: 'registration', component: RegistrationComponent },
  { path: 'myBookings', component: MyBookingsComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthPanelGuard]
})
export class AppRoutingModule { }
