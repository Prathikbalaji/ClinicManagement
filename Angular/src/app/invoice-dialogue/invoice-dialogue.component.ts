import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';


@Component({
  selector: 'app-invoice-dialogue',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  template: `
    <h2 mat-dialog-title>Invoice Generated</h2>
    <div mat-dialog-content>Invoice has been sent to the patient.</div>
    <div mat-dialog-actions>
      <button mat-button mat-dialog-close>Close</button>
    </div>
  `,
  styleUrl: './invoice-dialogue.component.css'
})
export class InvoiceDialogueComponent {

}
