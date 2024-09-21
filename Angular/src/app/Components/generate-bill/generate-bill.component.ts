import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';
import { MatDialogModule } from '@angular/material/dialog';
import { materialize } from 'rxjs';
import { InvoiceDialogueComponent } from '../../invoice-dialogue/invoice-dialogue.component';
import { MatDialog } from '@angular/material/dialog';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-generate-bill',
  standalone: true,
  imports: [MatDialogModule,CommonModule,ReceptionistHeaderComponent],
  templateUrl: './generate-bill.component.html',
  styleUrl: './generate-bill.component.css'
})
export class GenerateBillComponent {
  appointments : any[] = [] 

  constructor(private recService : ReceptionistService,private router : Router,private dialog : MatDialog,private cdr: ChangeDetectorRef){}
  ngOnInit(): void {
    this.recService.GetPatientRecordsAfterReview().subscribe(
      (data) => {
        this.appointments = data;
      },
      (error) => console.error('Error loading Appointments', error)
    );
  }

  generateBill(appointment: any) {
    this.recService.genarateInvoice(appointment.patientID, appointment.appointmentId).subscribe(
      (data) => {
        console.log('Invoice generated successfully', data);
        this.dialog.open(InvoiceDialogueComponent);
        this.recService.GetPatientRecordsAfterReview().subscribe(
          (data) => {
            this.appointments = data;
          },
          (error) => console.error('Error loading Appointments', error)
        );
      },
      (error) => {
        console.error('Error generating invoice:', error);
      }
    );
  }
  

}
