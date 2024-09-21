import { Component } from '@angular/core';
import { ReceptionistService } from '../../Services/receptionist.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-delete-patient',
  standalone: true,
  imports: [],
  templateUrl: './delete-patient.component.html',
  styleUrl: './delete-patient.component.css'
})
export class DeletePatientComponent {

  constructor(private recService : ReceptionistService,private route:ActivatedRoute){

  }

  ngOnInit():void{
    const id = +this.route.snapshot.params['id'];
    this.recService.DeletePatient(id).subscribe(
    )
  }

}
