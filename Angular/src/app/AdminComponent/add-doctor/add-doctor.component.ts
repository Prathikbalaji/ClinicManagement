import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AdminServiceService } from '../../Services/admin-service.service';
import { Router } from '@angular/router';
import { AdminHeaderComponent } from '../admin-header/admin-header.component';

@Component({
  selector: 'app-add-doctor',
  standalone: true,
  imports: [CommonModule,FormsModule,AdminHeaderComponent],
  templateUrl: './add-doctor.component.html',
  styleUrl: './add-doctor.component.css'
})
export class AddDoctorComponent {

  constructor(private admSer : AdminServiceService,private router : Router){}
  
  doctor = {
    userName: '',
    password: '',
    roleID: null,
    doctorName: '',
    age: null,
    specialization: '',
    email: '',
    phoneNumber: '',
    consultantFee : ''
  };

  errorMessage : string | null = null


  onSubmit(form: NgForm) {
    
      this.admSer.addDoctor(this.doctor).subscribe(
        response => {
          this.errorMessage = null;
          console.log('Form submitted successfully', response);
          this.router.navigate(['/GetDoctors']);
        },
        (error) => {
          if (error.error && error.error.message) {
            this.errorMessage = error.error.message; 
          } 
        }
      );
    }
  }



