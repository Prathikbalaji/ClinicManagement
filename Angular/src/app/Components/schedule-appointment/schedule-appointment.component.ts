import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Appointments } from '../Appointments';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';
import { CommonDialogueComponent } from '../../common-dialogue/common-dialogue.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-schedule-appointment',
  standalone: true,
  imports: [CommonModule,FormsModule,ReceptionistHeaderComponent],
  templateUrl: './schedule-appointment.component.html',
  styleUrl: './schedule-appointment.component.css'
})
export class ScheduleAppointmentComponent {

  patients: any[] = [];

  doctor :Doctor[] = []

  Specialization : string[] =[]

  AvailDates : string[] = []

  selectedPatient : number = 0;

  Spec : string = ''

  DocID : number = 0

  SelectedDate : string = ''

  Reason : string = ''

  ApntStatus : string = "Scheduled"

  appointment ? : Appointments 

  constructor(private dialog : MatDialog ,private receptionistService: ReceptionistService, private router: Router) { }

  selectedFileBase64: string = ''

  errorMessage: string | null = null;


  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.selectedFileBase64 = reader.result as string; // Store file as Base64 string
      };
      reader.readAsDataURL(file); // Convert file to Base64
    }
  }

  saveAppointment():void{
    this.appointment = {
      appointmentDate: this.SelectedDate,
      appointmentStatus: this.ApntStatus,
      reasonForVisit: this.Reason,
      patientID: this.selectedPatient,
      doctorID: this.DocID,
      testRecord: this.selectedFileBase64 == '' ? '' : this.selectedFileBase64
    };
    this.receptionistService.AddAppointment(this.appointment).subscribe(
      (response)=>{
        this.dialog.open(CommonDialogueComponent, {
          data: {
            title: 'Appointment Scheduling',
            message: 'Appointment Scheduled Successfully!',
          },
        });
        this.errorMessage = null;
      },
      (error) => {
        if (error.error && error.error.message) {
          this.errorMessage = error.error.message; 
        } else {
          this.errorMessage = 'An unexpected error occurred. Please try again later.'; // Fallback error
        }
      }
    )
  }

  ngOnInit(): void {

    this.receptionistService.getPatients().subscribe(
      (data) => {
        this.patients = data;
      },
      (error) => console.error('Error loading Patients', error)
    );

    this.receptionistService.getSpecilization().subscribe(
      (data) => {
        this.Specialization = data;
      },
      (error) => console.error('Error Getting Specilization', error)
    );
  }

  onSpecializationChange(str: string): void {

      this.receptionistService.GetDoctorsBySpecialization(str).subscribe(
        (doctors) => {
          this.doctor = doctors;
        },
        (error) => console.error('Error fetching doctors', error)
      );
  }

  OnDoctorChanges(DocId: number): void {
    this.receptionistService.GetAvailDatesByDoctor(DocId).subscribe(
      (data) => {
        this.AvailDates = data
          .map((dateString: string) => {
            const date = new Date(dateString);
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
          })
          .sort((a: string, b: string) => {
            // Convert the formatted string back to Date objects for sorting
            return new Date(a).getTime() - new Date(b).getTime();
          });
      },
      (error) => console.error('Error fetching doctors', error)
    );
  }
  

  

}

interface Doctor{
  doctorID : number
  doctorName : string
}


