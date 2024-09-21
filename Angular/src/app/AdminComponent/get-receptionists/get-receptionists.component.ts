import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminServiceService } from '../../Services/admin-service.service';
import { CommonModule } from '@angular/common';
import { AdminHeaderComponent } from '../admin-header/admin-header.component';

@Component({
  selector: 'app-get-receptionists',
  standalone: true,
  imports: [CommonModule,AdminHeaderComponent],
  templateUrl: './get-receptionists.component.html',
  styleUrl: './get-receptionists.component.css'
})
export class GetReceptionistsComponent {
  recs : any[] = [] 

  constructor(private admin : AdminServiceService,private router : Router){}

  ngOnInit(): void {
    this.admin.getReceptionists().subscribe(
      (data) => {
        this.recs = data;
      },
      (error) => console.error('Error loading Receptionists', error)
    );
  }
}
