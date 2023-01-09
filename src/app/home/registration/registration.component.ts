import { Component, OnInit } from '@angular/core';
import { Registration } from 'src/app/shared/registration.model';
import { RegistrationService } from '../../shared/registration.service'
import { faEye } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registration: Registration = new Registration();
  faEyeIcon = faEye;
  type: any = "password";
  check: boolean = false;

  CheckBoxStatus() {
    if (this.check == false) {
      this.check = true;
    }
    else {
      this.check = false;
    }
  }

  showPassword() {
    if (this.type == "password") {
      this.type = "text";
    }
    else {
      this.type = "password";
    }
  }

  constructor(public service: RegistrationService) { }

  Register() {
    this.service.postRegistration(this.registration);
  }

  ngOnInit(): void {

  }

}
