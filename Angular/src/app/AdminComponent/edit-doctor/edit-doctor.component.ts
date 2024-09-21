import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AdminHeaderComponent } from '../admin-header/admin-header.component';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminServiceService } from '../../Services/admin-service.service';

@Component({
  selector: 'app-edit-doctor',
  standalone: true,
  imports: [CommonModule,FormsModule,AdminHeaderComponent],
  templateUrl: './edit-doctor.component.html',
  styleUrl: './edit-doctor.component.css'
})
export class EditDoctorComponent {
  constructor(private admSer : AdminServiceService,private route:ActivatedRoute,private router : Router){
  }

  doc = {
    doctorID : null,
    doctorName: '',
    age: null,
    specialization: '',
    email: '',
    phoneNumber: '',
    consultantFee : ''
  };

  ngOnInit(): void {
    const id = +this.route.snapshot.params['id'];
    this.admSer.getDoctorById(id).subscribe(
      (response) => {
        this.doc = response
      }
    )
  }

  onSubmit(){
    console.log(this.doc)
    this.admSer.editDoctor(this.doc.doctorID,this.doc).subscribe(
      response => {
        console.log('Form submitted successfully', response);
        this.router.navigate(['/GetDoctors']);
      },
      (error) => {
        console.error('Error occurred:', error);
      }
    );
  }

}
