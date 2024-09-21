import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ReceptionistComponent } from './Components/receptionist/receptionist.component';
import { ReceptionistHeaderComponent } from './Components/receptionist-header/receptionist-header.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,ReceptionistComponent,ReceptionistHeaderComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true, // Ensures that multiple interceptors can be used
    },
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ClinicMgmt';
}
