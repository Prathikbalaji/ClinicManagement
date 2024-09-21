import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-common-dialogue',
  standalone: true,
  imports: [],
  templateUrl: './common-dialogue.component.html',
  styleUrl: './common-dialogue.component.css'
})
export class CommonDialogueComponent {
  constructor(
    public dialogRef: MatDialogRef<CommonDialogueComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  onClose(): void {
    this.dialogRef.close();
  }
}
