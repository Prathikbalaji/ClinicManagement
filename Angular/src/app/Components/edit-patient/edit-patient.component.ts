import { CommonModule, DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReceptionistService } from '../../Services/receptionist.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Patient } from '../Patient';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';

@Component({
  selector: 'app-edit-patient',
  standalone: true,
  imports: [CommonModule,FormsModule,ReceptionistHeaderComponent],
  templateUrl: './edit-patient.component.html',
  styleUrl: './edit-patient.component.css'
})
export class EditPatientComponent {
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

  constructor(private recService : ReceptionistService,private route:ActivatedRoute,private router : Router){

  }

  ngOnInit():void{
    const id = +this.route.snapshot.params['id'];
    this.recService.getPatientById(id).subscribe(
      (response) => {
        this.patient = response
        this.patient.dateOfBirth = this.extractDateFromISO(response.dateOfBirth);
      }
    )
  }
  
  extractDateFromISO(isoString: string): string {
    return isoString.split('T')[0]; 
  }

  onSubmit() : void{
    this.recService.editPatient(this.patient.patientID,this.patient).subscribe(
      (response)=>{
        console.log('Updated!',response);
        this.router.navigate(['/Receptionist']);
      }
    )
  };

}
