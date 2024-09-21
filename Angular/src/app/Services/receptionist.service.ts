import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Patient } from '../Components/Patient';
import {Appointments} from '../Components/Appointments';

@Injectable({
  providedIn: 'root'
})
export class ReceptionistService {
  
  private baseUrl ="https://localhost:7199/api/Receptionist"

  constructor(private http:HttpClient){

  }

  getPatients(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetPatients`);
  }

  getSpecilization(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetSpecilizations`);
  }

  GetDoctorsBySpecialization(Specl: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetDoctorsBySpecilization`, {
      params: { Specialization: Specl }
    });
  }

  GetAvailDatesByDoctor(DocId : number) :  Observable<any> {
    return this.http.get(`${this.baseUrl}/GetAvailDatesByDoctor`, {
      params: { doc: DocId }
    });
  }

  AddPatient(Pat : Patient) : Observable<any>{
    return this.http.post(`${this.baseUrl}/AddPatient`,Pat);
  }

  AddAppointment(App : Appointments) : Observable<any>{
    return this.http.post(`${this.baseUrl}/ScheduleAppointment`,App);
  }

  GetAllDoctorAppointments() : Observable<any>{
    return this.http.get(`${this.baseUrl}/GetAllDoctorAppointments`);
  }

  CancelAppointment(id : number) :  Observable<any> {
    return this.http.put(`${this.baseUrl}/CancelAppointment/${id}`, null);
  }

  getPatientById(id : number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetPatientById/${id}`);
  }

  editPatient(id:number,Pat : Patient):Observable<any>{
    return this.http.put(`${this.baseUrl}/UpdatePatient/${id}`,Pat);
  }

  DeletePatient(id:number):Observable<any>{
    return this.http.delete(`${this.baseUrl}/DeletePatient/${id}`);
  }

  GetPatientRecordsAfterReview() : Observable<any> {
    return this.http.get(`${this.baseUrl}/GetPatientRecordsAfterReview`);
  }

  genarateInvoice(Pid : number,Aid : number): Observable<any> {
    return this.http.post(`${this.baseUrl}/GenerateInvoice/${Pid}/${Aid}`,'');
  }

}

