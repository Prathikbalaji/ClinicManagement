import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';

@Component({
  selector: 'app-all-appointments',
  standalone: true,
  imports: [CommonModule, FormsModule, ReceptionistHeaderComponent],
  templateUrl: './all-appointments.component.html',
  styleUrl: './all-appointments.component.css'
})
export class AllAppointmentsComponent {

  appointments: any[] = []; // Holds all appointments
  currentPage: number = 1; // Start on the first page
  pageSize: number = 10;   // Show 10 items per page
  totalPages: number = 0;  // Total number of pages
  
  constructor(private recService: ReceptionistService, private router: Router) {}

  ngOnInit(): void {
    this.recService.GetAllDoctorAppointments().subscribe(
      (data) => {
        // Sort appointments by appointmentDate in descending order
        this.appointments = data.sort((a : any, b : any) => {
          return new Date(b.appointmentDate).getTime() - new Date(a.appointmentDate).getTime();
        });

        // Calculate the total pages based on the number of appointments
        this.totalPages = Math.ceil(this.appointments.length / this.pageSize);
      },
      (error) => console.error('Error loading Appointments', error)
    );
  }

  extractDateFromISO(isoString: string): string {
    return isoString.split('T')[0]; // Extract only the date part
  }

  cancelAppointment(id: number): void {
    console.log(id);
    if (confirm('Are you sure you want cancel this Appointment?')) {
      this.recService.CancelAppointment(id).subscribe(
        (response) => {
          console.log('Updated!', response);
          // Refresh the appointments list without reloading the entire page
          this.loadAppointments();
        },
        (error) => console.error('Error cancelling appointment', error)
      );
    }
  }

  loadAppointments(): void {
    this.recService.GetAllDoctorAppointments().subscribe(
      (data) => {
        // Sort and update appointments
        this.appointments = data.sort((a : any, b : any) => {
          return new Date(b.appointmentDate).getTime() - new Date(a.appointmentDate).getTime();
        });
        this.totalPages = Math.ceil(this.appointments.length / this.pageSize);
      },
      (error) => console.error('Error loading appointments', error)
    );
  }

  // Method to generate bill (currently empty)
  generateBill(id: number): void {
    // Add logic to generate a bill
  }
}
