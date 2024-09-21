import { Component } from '@angular/core';
import { DoctorService } from '../../Services/doctor.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DoctorHeaderComponent } from '../doctor-header/doctor-header.component';
import { Observable } from 'rxjs';
import { ReceptionistService } from '../../Services/receptionist.service';

@Component({
  selector: 'app-todays-appointments',
  standalone: true,
  imports: [DoctorHeaderComponent, CommonModule, FormsModule],
  templateUrl: './todays-appointments.component.html',
  styleUrl: './todays-appointments.component.css'
})
export class TodaysAppointmentsComponent {

  appointments : any[] = [] 
  
  id: number = Number(localStorage.getItem('User'));



  constructor(private docService : DoctorService,private router : Router,private recSer : ReceptionistService){}

  loadAppointments(){
    this.docService.getTodaysAppointments(this.id).subscribe(
      (data) => {
        this.appointments = data;
        
      },
      (error) => console.error('Error loading Appointments', error)
    );
  }
  
  ngOnInit(): void {
    this.loadAppointments();
  }

  AddReview(AppID : number){
    this.router.navigate(['/AddReview', AppID]);
  }

  CancelApp(AppID : number) {
    this.recSer.CancelAppointment(AppID).subscribe(
      (response) => {
        console.log('Updated!', response);
        this.loadAppointments();
      },
      (error) => console.error('Error cancelling appointment', error)
    );
  }

}
