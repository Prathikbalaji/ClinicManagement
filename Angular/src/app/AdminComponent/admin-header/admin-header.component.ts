import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-header',
  standalone: true,
  imports: [],
  templateUrl: './admin-header.component.html',
  styleUrl: './admin-header.component.css'
})
export class AdminHeaderComponent {
  UName = localStorage.getItem('UName')

  constructor(private router: Router) { }

  viewdocs(){
    this.router.navigate(['/GetDoctors']);
  }
  viewrecs(){
    this.router.navigate(['/GetReceptionists']);
  }
  createdoc(){
    this.router.navigate(['/addDoctor']);
  }
  
  createrec(){
    this.router.navigate(['/addReceptionist']);
  }

  getReports(){
    this.router.navigate(['/AdminReports']);
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
