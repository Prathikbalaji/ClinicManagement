import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Patient } from '../Patient';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';
import { MatDialog } from '@angular/material/dialog';
import { CommonDialogueComponent } from '../../common-dialogue/common-dialogue.component';
@Component({
  selector: 'app-add-patient',
  standalone: true,
  imports: [CommonModule,FormsModule,ReceptionistHeaderComponent],
  templateUrl: './add-patient.component.html',
  styleUrl: './add-patient.component.css'
})
export class AddPatientComponent {

  constructor(private dialog : MatDialog, private RecService : ReceptionistService,private route:ActivatedRoute,private router : Router){

  }

  errorMessage : string | null = null

  patient: Patient = {
    patientID : 0 , 
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    gender: '',
    phoneNumber: '',
    email: '',
    address: ''
  }; 

  onSubmit() : void{
    this.RecService.AddPatient(this.patient).subscribe(
      (response)=>{
        this.errorMessage = null;
        this.dialog.open(CommonDialogueComponent, {
          data: {
            title: 'Patient Creation',
            message: 'Patient Created Successfully!',
          },
          
        });
      },
      (error) => {
        if (error.error && error.error.message) {
          this.errorMessage = error.error.message; 
        } else {
          this.errorMessage = 'An unexpected error occurred. Please try again later.'; // Fallback error
        }
      }
    )
  };



}
