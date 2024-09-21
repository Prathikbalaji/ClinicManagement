import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-header',
  standalone: true,
  imports: [],
  templateUrl: './doctor-header.component.html',
  styleUrl: './doctor-header.component.css'
})
export class DoctorHeaderComponent {
  UName = localStorage.getItem('UName')

  constructor(private router: Router) { }

  appointments(){
    this.router.navigate(['/TodaysAppointments']);
  }
  
  updAvailabilities(){
    this.router.navigate(['/UpdateAvailability']);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('User');
    localStorage.removeItem('UserId');
    localStorage.removeItem('role');
    localStorage.removeItem('UName');
    this.router.navigate(['/']);
  }

}
