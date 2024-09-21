import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DoctorService } from '../../Services/doctor.service';
import { Router } from '@angular/router';
import { DoctorHeaderComponent } from '../doctor-header/doctor-header.component';

@Component({
  selector: 'app-update-availability',
  standalone: true,
  imports: [CommonModule,FormsModule,DoctorHeaderComponent],
  templateUrl: './update-availability.component.html',
  styleUrl: './update-availability.component.css'
})
export class UpdateAvailabilityComponent implements OnInit {

   currentDate = new Date();
   currentMonth = this.currentDate.getMonth(); // Returns a zero-based index (0 for January, 1 for February, etc.)

// To get the month as a string (e.g., "January", "February", etc.):
 monthNames = [
  'January', 'February', 'March', 'April', 'May', 'June',
  'July', 'August', 'September', 'October', 'November', 'December'
];
 currentMonthName = this.monthNames[this.currentMonth];

  doctorId: string | null = localStorage.getItem('User');  
  calendarDays: any[] = [];
  daysOfWeek: string[] = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];  // Days of the week

  selectedDay: any = null;

  constructor(private availabilityService: DoctorService,private router : Router) { }

  ngOnInit(): void {
    this.generateCalendar();
    this.fetchDoctorAvailability();  
  }

  fetchDoctorAvailability(): void {
    const today = new Date(); 
    today.setHours(0, 0, 0, 0); 
  
    this.availabilityService.getAvailability(this.doctorId).subscribe((data) => {
      this.calendarDays.forEach(day => {
        if (!day.date) return;
  
        const savedDay = data.find((d: any) => {
          const availableDate = new Date(d.availableDate);
          availableDate.setHours(0, 0, 0, 0); 
  
          return availableDate.getFullYear() === day.date.getFullYear() &&
                 availableDate.getMonth() === day.date.getMonth() &&
                 availableDate.getDate() === day.date.getDate();
        });
  
        if (savedDay && day.date >= today) {
          day.availability = savedDay.availabilityStatus === 'Available';
        }
      });
    });
  }  

  closeModal(): void {
    this.selectedDay = null; 
  }
  
  
  
  selectDay(day: any): void {
    if (!day.isCurrentMonth || day.isPast) return; // Prevent selection of past dates
    this.calendarDays.forEach(d => d.isSelected = false);
    day.isSelected = true;
    this.selectedDay = day ; // Create a copy of the selected day to avoid mutation issues
  }
  
  

  // Save the doctor's updated availability
  saveAvailability(): void {
    if (this.selectedDay && this.selectedDay.availability) {
      // Only save the selected day if it's marked as available
      const availabilityData = [{
        AvailableDate: this.selectedDay.date.toLocaleDateString('en-CA'),  // Format date to 'YYYY-MM-DD'
        AvailabilityStatus: 'Available',  // Set status as 'Available'
        DoctorID: this.doctorId  // Use the doctor's ID
      }];
  
      // Check what's being sent
      console.log(availabilityData);
  
      this.availabilityService.saveAvailability(this.doctorId, availabilityData).subscribe((response) => {
        console.log('Availability saved successfully');
        this.router.navigate(['/UpdateAvailability']);
      });
  
      // Reset selected day after saving
      this.selectedDay.isSelected = false;
      this.selectedDay = null;
    }
    else{

      console.log(this.selectedDay.date.toLocaleDateString('en-CA'));

      this.availabilityService.deleteAvailability(this.doctorId, this.selectedDay.date.toLocaleDateString('en-CA')).subscribe(() => {

        console.log('Availability Deleted successfully');
        this.router.navigate(['/UpdateAvailability']);
      });

      this.selectedDay.isSelected = false;
      this.selectedDay = null;
    }
  }
  
  
  generateCalendar(): void {
    const today = new Date(); // Current date
    const firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    const lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);
    const startDay = firstDayOfMonth.getDay();
    const totalDays = lastDayOfMonth.getDate();
  
    // Generate empty cells for days before the first of the month
    for (let i = 0; i < startDay; i++) {
      this.calendarDays.push({ date: null, isCurrentMonth: false });
    }
  
    // Generate days for the current month
    for (let i = 1; i <= totalDays; i++) {
      const date = new Date(today.getFullYear(), today.getMonth(), i);
  
      // Determine if the day is in the past
      const isPast = date < today && date.toDateString() !== today.toDateString();
  
      this.calendarDays.push({
        date,
        isCurrentMonth: true,
        availabilityStatus: null,  // Default to null (no status)
        isSelected: false,
        isPast: isPast  // Add a new flag for past days
      });
    }
  }
  
}