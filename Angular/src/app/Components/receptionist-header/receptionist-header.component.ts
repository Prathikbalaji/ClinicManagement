import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-receptionist-header',
  standalone: true,
  imports: [],
  templateUrl: './receptionist-header.component.html',
  styleUrl: './receptionist-header.component.css'
})
export class ReceptionistHeaderComponent {

  UName = localStorage.getItem('UName')

  constructor(private router: Router) { }

  allpatients(){
    this.router.navigate(['/Receptionist']);
  }

  registerPatient(){
    this.router.navigate(['/AddPatient']);
  }

  scheduleappointment(){
    this.router.navigate(['/ScheduleAppointment']);
  }

  allappointments(){
    this.router.navigate(['/AllAppointments']);
  }
  genbill(){
    this.router.navigate(['/genbill']);
  }
}
