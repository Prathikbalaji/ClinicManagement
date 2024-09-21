import { Component } from '@angular/core';
import { DoctorService } from '../../Services/doctor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Appointments } from '../../Components/Appointments';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MedicalRec } from '../../MedicalRec';
import { DoctorHeaderComponent } from '../doctor-header/doctor-header.component';

@Component({
  selector: 'app-add-review',
  standalone: true,
  imports: [ DoctorHeaderComponent,CommonModule,FormsModule],
  templateUrl: './add-review.component.html',
  styleUrl: './add-review.component.css'
})
export class AddReviewComponent {

  appointments : any = {}

  Notes : string = ''

  base64Data: string | undefined = ''; 

  fileName = 'TestRecord.pdf';

  Pid: number = 0;
  IsBillGenerated : number = 0
  AppID : number = 0
  DocID = localStorage.getItem('User')


  Medic? : MedicalRec 

  constructor(private docService : DoctorService,private route:ActivatedRoute,private router : Router){

  }


  save(){
    const doc : number = Number(this.DocID);
    this.Medic = {
      medicalNotes : this.Notes,
      isBillGenerated : this.IsBillGenerated,
      patientID : this.Pid,
      appointmentID : this.AppID,
      doctorID : doc
    };
    this.docService.SubmitReview(this.Medic).subscribe(
      (response)=>{
             console.log(response)
             this.router.navigate(['/TodaysAppointments']);
      }      
    )
  }

  downloadTestRecord(base64Data: string | undefined, fileName: string): void {
    if (!base64Data) {
      console.error('Base64 data is undefined or null.');
      return;
    }
  
    // Extract the MIME type if available
    const base64PrefixPattern = /^data:(.+);base64,/;
    const matches = base64Data.match(base64PrefixPattern);
    let mimeType = '';
    let extension = '';
  
    if (matches) {
      mimeType = matches[1]; // Extract MIME type (e.g., 'application/pdf', 'image/png')
      base64Data = base64Data.substring(matches[0].length); // Remove the MIME type prefix from the base64 string
  
      // Determine the file extension based on the MIME type
      switch (mimeType) {
        case 'application/pdf':
          extension = '.pdf';
          break;
        case 'image/png':
          extension = '.png';
          break;
        case 'image/jpeg':
          extension = '.jpeg';
          break;
        // Add more cases for other file types as needed
        default:
          extension = '';  // Fallback in case of an unknown MIME type
      }
    }
  
    try {
      const byteCharacters = atob(base64Data);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
      const blob = new Blob([byteArray], { type: mimeType || 'application/octet-stream' });
  
      const link = document.createElement('a');
      link.href = URL.createObjectURL(blob);
      link.download = fileName || `DownloadedFile${extension}`;  // Use dynamic file extension
      link.click();
    } catch (error) {
      console.error('Error decoding Base64 data:', error);
    }
  }
  
  
  

  download(): void {
    console.log('hi')
      this.downloadTestRecord(this.base64Data, this.fileName);
    
  }

  ngOnInit(): void {
    const id = +this.route.snapshot.params['id'];

    this.docService.getAppointmentsById(id).subscribe(
      (response) => {
        if(response && response.length){
        console.log('API Response:', response);

        this.appointments = response;
        this.Pid = response[0].patientID;
        this.AppID = response[0].appointmentId;

        console.log(this.Pid +  "" + this.AppID)

        if(response[0].testRecord){
          this.base64Data = response[0].testRecord.trim();
        }
        else {
          this.base64Data = undefined;
        }
      }
      },
      (error) => {
        console.error('Error fetching appointment:', error);
      }
    );
  }
}


/*public string? MedicalNotes { get; set; }

[Required]
public int IsBillGenerated { get; set; } = 0;

// Foreign key relationships
[ForeignKey("PatientID")]
public int PatientID { get; set; }

[ForeignKey("AppointmentID")]
public int AppointmentID { get; set; }

[ForeignKey("DoctorID")]
public int DoctorID { get; set; }

AppointmentId = a.AppointmentID,
PatientName = a.PatientName,
Age = CalculateAge(a.DateOfBirth, currentDate),
AppointmentDate = a.AppointmentDate,
ReasonForVisit = a.ReasonForVisit,
TestRecord = a.TestRecord

*/