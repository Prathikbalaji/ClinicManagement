import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Patient } from '../Patient';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';

@Component({
  selector: 'app-receptionist',
  standalone: true,
  imports: [CommonModule, FormsModule,ReceptionistHeaderComponent],
  templateUrl: './receptionist.component.html',
  styleUrl: './receptionist.component.css'
})
export class ReceptionistComponent {

  patients: any[] = [];
  filteredPatients: any[] = [];
  searchQuery: string = '';
  currentPage = 1;
  pageSize = 10;

  constructor(private receptionistService: ReceptionistService, private router: Router) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.receptionistService.getPatients().subscribe(
      (data) => {
        this.patients = data;
        this.filteredPatients = this.patients; // Initialize filteredPatients with all patients
      },
      (error) => console.error('Error loading Patients', error)
    );
  }

  totalPages: number = Math.ceil(this.filterPatients.length / this.pageSize);

  filterPatients(): void {
    this.filteredPatients = this.patients.filter(patient =>
      `${patient.firstName} ${patient.lastName}`.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }

  viewPatient(id: number): void {
    this.router.navigate(['/ViewPatient', id]);
  }

  editPatient(id: number): void {
    this.router.navigate(['/EditPatient', id]);
  }

  deletePatient(id: number): void {
    if (confirm('Are you sure you want to delete this Patient?')) {
      this.receptionistService.DeletePatient(id).subscribe(
        () => {
          this.loadPatients();
        },
        (error) => console.error('Error deleting patient', error)
      );
    }
  }
}
