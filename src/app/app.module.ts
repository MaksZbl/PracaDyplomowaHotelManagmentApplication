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
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
