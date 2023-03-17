import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './home/header/header.component';
import { IntroComponent } from './home/intro/intro.component';
import { MotionContentComponent } from './home/motion-content/motion-content.component';
import { GaleryComponent } from './home/galery/galery.component';
import { FooterComponent } from './home/footer/footer.component';
import { CopyrightComponent } from './home/copyright/copyright.component';
import { HomeComponent } from './home/home.component';
import { HotelsComponent } from './hotels/hotels.component';
import { HotelContentComponent } from './hotels/hotel-content/hotel-content.component';
import { HttpClientModule } from '@angular/common/http';
import { OverlayModule } from '@angular/cdk/overlay';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EmployeeComponent } from './Employee/employee/employee.component';
import { EmployeenavComponent } from './Employee/employeenav/employeenav.component';
import { EmployeepanelroomsComponent } from './Employee/employee/employeepanelrooms/employeepanelrooms.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HotelOverviewComponent } from './hotels/hoteloverview/hotel.overview.component';
import { MdbAccordionModule } from 'mdb-angular-ui-kit/accordion';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { MdbCollapseModule } from 'mdb-angular-ui-kit/collapse';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';
import { MdbModalModule } from 'mdb-angular-ui-kit/modal';
import { MdbPopoverModule } from 'mdb-angular-ui-kit/popover';
import { MdbRadioModule } from 'mdb-angular-ui-kit/radio';
import { MdbRangeModule } from 'mdb-angular-ui-kit/range';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { MdbScrollspyModule } from 'mdb-angular-ui-kit/scrollspy';
import { MdbTabsModule } from 'mdb-angular-ui-kit/tabs';
import { MdbTooltipModule } from 'mdb-angular-ui-kit/tooltip';
import { MdbValidationModule } from 'mdb-angular-ui-kit/validation';
import { BookingComponent } from './Booking/booking/booking.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { RegistrationComponent } from './home/registration/registration.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';
import { PaymentDetailComponent } from './payment-detail/payment-detail.component';
import { BookingService } from './shared/booking.service';
import { RegistrationService } from './shared/registration.service';
import { HotelService } from './shared/hotel.service';
import { PaymentService } from './shared/payment.service';
import { RateService } from './shared/rate.service';
import { MyPaymentsComponent } from './my-payments/my-payments.component';
import { EmployeepanelpaymentsComponent } from './Employee/employee/employeepanelpayments/employeepanelpayments.component';
import { EmployeePanelUsersComponent } from './Employee/employee/employee-panel-users/employee-panel-users.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    IntroComponent,
    MotionContentComponent,
    GaleryComponent,
    FooterComponent,
    CopyrightComponent,
    HomeComponent,
    HotelsComponent,
    HotelContentComponent,
    EmployeeComponent,
    EmployeenavComponent,
    EmployeepanelroomsComponent,
    HotelOverviewComponent,
    BookingComponent,
    RegistrationComponent,
    MyBookingsComponent,
    PaymentDetailComponent,
    MyPaymentsComponent,
    EmployeepanelpaymentsComponent,
    EmployeePanelUsersComponent,
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    OverlayModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    MdbAccordionModule,
    MdbCarouselModule,
    MdbCheckboxModule,
    MdbCollapseModule,
    MdbDropdownModule,
    MdbFormsModule,
    MdbModalModule,
    MdbPopoverModule,
    MdbRadioModule,
    MdbRangeModule,
    MdbRippleModule,
    MdbScrollspyModule,
    MdbTabsModule,
    MdbTooltipModule,
    MdbValidationModule,
    MatDialogModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatAutocompleteModule
  ],
  providers: [
    BookingService,
    RegistrationService,
    HotelService,
    PaymentService,
    RateService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
