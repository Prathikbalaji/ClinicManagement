import {  Routes } from '@angular/router';
import { ReceptionistComponent } from './Components/receptionist/receptionist.component';
import { ViewPatientComponent } from './Components/view-patient/view-patient.component';
import { EditPatientComponent } from './Components/edit-patient/edit-patient.component';
import { AddPatientComponent } from './Components/add-patient/add-patient.component';
import { ScheduleAppointmentComponent } from './Components/schedule-appointment/schedule-appointment.component';
import { AllAppointmentsComponent } from './Components/all-appointments/all-appointments.component';
import { TodaysAppointmentsComponent } from './DoctorComponents/todays-appointments/todays-appointments.component';
import { authGuard } from './auth.guard';
import { LoginComponent } from './LoginComponent/login/login.component';
import { AddReviewComponent } from './DoctorComponents/add-review/add-review.component';
import { roleGuard } from './role.guard';
import { UpdateAvailabilityComponent } from './DoctorComponents/update-availability/update-availability.component';
import { GenerateBillComponent } from './Components/generate-bill/generate-bill.component';
import { AddDoctorComponent } from './AdminComponent/add-doctor/add-doctor.component';
import { AddReceptionistComponent } from './AdminComponent/add-receptionist/add-receptionist.component';
import { GetDoctorsComponent } from './AdminComponent/get-doctors/get-doctors.component';
import { GetReceptionistsComponent } from './AdminComponent/get-receptionists/get-receptionists.component';
import { EditDoctorComponent } from './AdminComponent/edit-doctor/edit-doctor.component';
import { AdminReportsComponent } from './AdminComponent/admin-reports/admin-reports.component';


export const routes: Routes = [
    
    //   {
    //     path: 'admin',
    //     component: AdminComponent,
    //     canActivate: [roleGuard], // Apply RoleGuard here
    //     data: { requiredRole: 'Admin' } // Pass the required role to RoleGuard
    //   },
      {
        path: '',
        component: LoginComponent
      },
      {
        path: 'Receptionist',
        component: ReceptionistComponent,
        canActivate: [roleGuard], 
        data: { requiredRole: 'Receptionist' }
      },
      // {
      //   path: 'forbidden',
      //   component: ForbiddenComponent
      // },

    {path : 'ViewPatient/:id',component:ViewPatientComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Receptionist' }},
    {path : 'EditPatient/:id',component:EditPatientComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Receptionist' }},
    {path : 'AddPatient',component:AddPatientComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Receptionist' }},
    {path : 'ScheduleAppointment',component:ScheduleAppointmentComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Receptionist' }},
    {path : 'AllAppointments',component:AllAppointmentsComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Receptionist' }},
    {path : 'TodaysAppointments',component:TodaysAppointmentsComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Doctor' }},
    {path : 'AddReview/:id',component:AddReviewComponent,
      canActivate: [roleGuard], 
      data: { requiredRole: 'Doctor' }},

      {path : 'UpdateAvailability',component:UpdateAvailabilityComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Doctor' }
      },
      {path : 'genbill',component:GenerateBillComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Receptionist' }
      },
      {path : 'addDoctor',component:AddDoctorComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Admin' }
      },
      {path : 'addReceptionist',component:AddReceptionistComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Admin' }
      },
      {path : 'GetDoctors',component:GetDoctorsComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Admin' }
      },
      {path : 'GetReceptionists',component:GetReceptionistsComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Admin' }
      },
      {path : 'editDoctor/:id',component:EditDoctorComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Admin' }
      },
      {path : 'AdminReports',component:AdminReportsComponent,
        canActivate: [roleGuard],
        data: { requiredRole: 'Admin' }
      }
];


