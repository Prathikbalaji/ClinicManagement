import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReceptionistHeaderComponent } from '../receptionist-header/receptionist-header.component';

@Component({
  selector: 'app-view-patient',
  standalone: true,
  imports: [CommonModule,FormsModule,ReceptionistHeaderComponent],
  templateUrl: './view-patient.component.html',
  styleUrl: './view-patient.component.css'
})
export class ViewPatientComponent {
  patient : any;

  constructor(private recService : ReceptionistService,private route:ActivatedRoute){

  }

  ngOnInit():void{
    const id = +this.route.snapshot.params['id'];
    this.recService.getPatientById(id).subscribe(
      (response) => {
        this.patient = response
      }
    )
  }
}
