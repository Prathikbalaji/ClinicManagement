import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalRec } from '../MedicalRec';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  
  private baseUrl ="https://localhost:7199/api/Doctor"

  constructor(private http:HttpClient){

  }

  getTodaysAppointments(DocId : number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetDoctorAppointments/${DocId}`);
  }

  getMedicines() : Observable<any> {
    return this.http.get(`${this.baseUrl}/GetMedicine`);
  }

  getAppointmentsById(AppId : number): Observable<any> {
    return this.http.get(`${this.baseUrl}/getAppointmentsById/${AppId}`);
  }

  SubmitReview(med : any): Observable<any> {
    return this.http.post(`${this.baseUrl}/SaveReview`,med);
  }

  getAvailability(doctorId: string | null): Observable<any> {
    return this.http.get(`${this.baseUrl}/availability/${doctorId}`);
  }

  saveAvailability(doctorId: string | null, availability: any[]): Observable<any> {
    return this.http.post(`${this.baseUrl}/availability/${doctorId}`, availability, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  deleteAvailability(doctorId: string | null, date: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/deleteAvailability/${doctorId}/${date}`);
  }

}
