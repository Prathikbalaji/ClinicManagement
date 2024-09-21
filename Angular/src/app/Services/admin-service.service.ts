import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminServiceService {

  private apiUrl ="https://localhost:7199/api/Admins"

  constructor(private http: HttpClient) { }

  // Method to send the form data to the backend
  addDoctor(data: any): Observable<any> {
    console.log(data);
    return this.http.post(`${this.apiUrl}/AddDoctor`, data);
  }

  getDoctors() : Observable<any> {
    return this.http.get(`${this.apiUrl}/GetDoctors`);
  }

  addReceptionist(data: any): Observable<any> {
    console.log(data);
    return this.http.post(`${this.apiUrl}/AddReceptionist`, data);
  }

  getReceptionists() : Observable<any> {
    return this.http.get(`${this.apiUrl}/GetReceptionists`);
  }

  getDoctorById(id : number) : Observable<any> {
    return this.http.get(`${this.apiUrl}/GetDoctorById/${id}`);
  }

  editDoctor(id : number | null , data : any) : Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateDoctor/${id}`,data);
  }

  getAllAppointments(range : string , fromDate : string | null , toDate : string | null): Observable<any> {
    return this.http.get(`${this.apiUrl}/AppointmentReports/${range}/${fromDate}/${toDate}`);
  }

  getTotalFees() : Observable<any> {
    return this.http.get(`${this.apiUrl}/getTotalFees`);
  }

}
