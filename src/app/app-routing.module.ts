import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPanelGuard } from './Employee/Auth/auth-panel.guard';
import { EmployeeComponent } from './Employee/employee/employee.component';
import { EmployeepanelroomsComponent } from './Employee/employee/employeepanelrooms/employeepanelrooms.component';
import { HomeComponent } from './home/home.component';
import { HotelOverviewComponent } from './hotels/hoteloverview/hotel.overview.component';
import { HotelsComponent } from './hotels/hotels.component';

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
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthPanelGuard]
})
export class AppRoutingModule { }
