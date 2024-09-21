import { Component } from '@angular/core';
import { AdminServiceService } from '../../Services/admin-service.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AdminHeaderComponent } from '../admin-header/admin-header.component';


@Component({
  selector: 'app-get-doctors',
  standalone: true,
  imports: [CommonModule,AdminHeaderComponent],
  templateUrl: './get-doctors.component.html',
  styleUrl: './get-doctors.component.css'
})
export class GetDoctorsComponent {
  doctors : any[] = [] 

  constructor(private admin : AdminServiceService,private router : Router){}

  ngOnInit(): void {
    this.admin.getDoctors().subscribe(
      (data) => {
        this.doctors = data;
      },
      (error) => console.error('Error loading Appointments', error)
    );
  }

  editDoctor(id : number){
    console.log(id);
    this.router.navigate(['/editDoctor',id]);
  }
}
