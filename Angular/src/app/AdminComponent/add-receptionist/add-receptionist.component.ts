import { Component } from '@angular/core';
import { AdminServiceService } from '../../Services/admin-service.service';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AdminHeaderComponent } from '../admin-header/admin-header.component';

@Component({
  selector: 'app-add-receptionist',
  standalone: true,
  imports: [CommonModule,FormsModule,AdminHeaderComponent],
  templateUrl: './add-receptionist.component.html',
  styleUrl: './add-receptionist.component.css'
})
export class AddReceptionistComponent {
  constructor(private admSer : AdminServiceService,private router : Router){}
  
  rec = {
    userName: '',
    password: '',
    roleID: null,
    recName: '',
    phoneNumber: ''
  };


  onSubmit(form: NgForm) {
    
      this.admSer.addReceptionist(this.rec).subscribe(
        response => {
          console.log('Form submitted successfully', response);
          this.router.navigate(['/GetReceptionists']);
        },
        (error) => {
          console.error('Error occurred:', error);
        }
      );
    }

}
