import { Component } from '@angular/core';
import { AdminServiceService } from '../../Services/admin-service.service';
import { CommonModule } from '@angular/common';
import { AdminHeaderComponent } from '../admin-header/admin-header.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-reports',
  standalone: true,
  imports: [CommonModule,FormsModule, AdminHeaderComponent],
  templateUrl: './admin-reports.component.html',
  styleUrl: './admin-reports.component.css'
})
export class AdminReportsComponent {

  constructor(private admin : AdminServiceService){
  
  }
  ngOnInit(){
    
  }
  showSection: string = ''; // Tracks which section to display ('appointments' or 'fees')
    showDateFilter: boolean = false; // Controls whether the date filter is displayed
    selectedRange: string = ''; // Holds the selected date range option
    fromDate: string | null = null; // From date for the 'From-To' option
    toDate: string | null = null; // To date for the 'From-To' option

    allappnts: any[] = []; 
    totalFees: any[] = []; 

    openDateFilter(section: string) {
        this.showSection = section;
        this.showDateFilter = true;
    }

    setDateRange(range: string) {
      this.selectedRange = range;
    }

    submitDateFilter() {
      if (this.selectedRange === 'fromTo' && (!this.fromDate || !this.toDate)) {
          alert("Please select both from and to dates.");
          return;
      }
      if (this.selectedRange === 'thisMonth'){
        this.fromDate = '0';
        this.toDate = '0';
      }
      if (this.selectedRange === 'thisWeek'){
        this.fromDate = '0';
        this.toDate = '0' ;
      }
      //this.showDateFilter = false;
      if (this.showSection === 'appointments') {
          this.getOverallAppointments();
      } 
  }

  cancelDateFilter() {
    this.showDateFilter = false;
    this.selectedRange = '';
    this.fromDate = null;
    this.toDate = null;
}

getOverallAppointments() {

  const filter: FilterDTO = {
    range: this.selectedRange,
    fromDate: this.fromDate,
    toDate: this.toDate
  };

  console.log(filter)

  this.admin.getAllAppointments(filter.range , filter.fromDate , filter.toDate ).subscribe(
        (data) => {
          this.allappnts = data.sort((a: any, b: any) => {
            return new Date(a.appointmentDate).getTime() - new Date(b.appointmentDate).getTime();
          });
        },
        (error) => console.error('Error loading Appointments', error)
      );
    }

  getTotalFee(){
    this.showDateFilter = false;
    this.selectedRange = '';
    this.fromDate = null;
    this.toDate = null;
    this.showSection = 'fees';
    this.admin.getTotalFees().subscribe(
      (data) => {
        this.totalFees = data;
      },
      (error) => console.error('Error loading Appointments', error)
    );
  }
}

interface FilterDTO{
  range : string,
  fromDate : string | null,
  toDate : string | null
}