import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { login } from '../../login';
import { LoginService } from '../../Services/login.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: `./login.component.html`,
  styleUrl: './login.component.css'
})
export class LoginComponent {
  user: login = {
    UserName:'',
    Password: '',
  }

  constructor(private authService: LoginService, private router: Router) { }

  login() {
    console.log(this.user.UserName, this.user.Password);
    if (this.user.UserName && this.user.Password) {
      this.authService.login(this.user).subscribe(
        data => {
         console.log(data)
         const token = data?.token;
         console.log(token) // Directly get the token from API response
          if (token) {
            localStorage.setItem('token', token); // Store the token directly
            const decodedToken: any = jwtDecode(token);
            localStorage.setItem('UserId', decodedToken.UserId);
            localStorage.setItem('role', decodedToken.role); 
            localStorage.setItem('UName', decodedToken.Uname); 
            console.log('Token stored');
            localStorage.setItem('User', decodedToken.User);
            console.log(localStorage.getItem('token'));
            if(localStorage.getItem('role') == 'Doctor'){
              console.log('came')
              this.router.navigate(['/TodaysAppointments']);
            }
            else if(localStorage.getItem('role') == 'Receptionist'){
              this.router.navigate(['/Receptionist']); 
            }
            else if(localStorage.getItem('role') == 'Admin'){
              this.router.navigate(['/GetDoctors']); 
            }
            
          } else {
            console.log('No token found in API response.');
            alert("Failed to retrieve token.");
          }        
        },
        error => {
          console.error('Login error:', error);
          alert("Invalid login details or server error");
        }
      );
    } else {
      alert("Please enter username and password first");
    }
  }
  register() {
    this.router.navigate(['register']);
  }
}
