using ClinicManagement.Interface;
using ClinicManagement.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Numerics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Globalization;

namespace ClinicManagement.Repository
{
    public class ReceptionistRepository : IReceptionist
    {

        private readonly ClinicManagementDBContext _dbContext;

        public ReceptionistRepository(ClinicManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }      

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _dbContext.Patients.Where(pat => pat.isDeleted == 0).ToListAsync();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _dbContext.Patients.Where(pat => pat.isDeleted == 0).FirstOrDefaultAsync(a => a.PatientID == id) ?? new Patient();
        }

        public async Task<IEnumerable<string>> GetSpecilizations()
        {

            return await _dbContext.Doctors
                           .Where(d => d.Specialization != null)
                           .Select(d => d.Specialization)
                           .Distinct()
                           .ToListAsync();
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = await _dbContext.Doctors
                .Where(d => d.Specialization == specialization)
                .Select(d => new DoctorDto
                {
                    DoctorID = d.DoctorID,
                    DoctorName = d.DoctorName
                })
                .ToListAsync();

            return doctors;
        }


        public async Task<IEnumerable<DateTime>> GetAvailDatesByDoctor(int doc)
        {
            var doctor = await _dbContext.Doctors
                                         .FirstOrDefaultAsync(d => d.DoctorID == doc);

            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
            var today = DateTime.Today;

            return await _dbContext.DoctorAvailabilities
                                   .Where(d => d.DoctorID == doctor.DoctorID && d.AvailableDate >= today)
                                   .Select(d => d.AvailableDate)
                                   .ToListAsync();
        }



        public async Task AddPatient(Patient patient)
        {

            var Pat = _dbContext.Patients.FirstOrDefault(a => a.Email == patient.Email && a.isDeleted == 0);

            if(Pat != null)
            {
                throw new Exception("Email Already Exists ! ");
            }

            await _dbContext.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePatient(int id, Patient patient)
        {
            var existingPatient = await _dbContext.Patients.FindAsync(id);

            if (existingPatient == null)
            {
                throw new ArgumentException("Patient not found");
            }

            existingPatient.FirstName = patient.FirstName;
            existingPatient.LastName = patient.LastName;
            existingPatient.Email = patient.Email;
            existingPatient.Address = patient.Address;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.Gender = patient.Gender;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePatient(int id)
        {

            Patient pat = await _dbContext.Patients.FindAsync(id);

            pat.isDeleted = 1;

            await _dbContext.SaveChangesAsync();
        }

        public async Task ScheduleAppointment(Appointment appointment) // need to send mail
        {

            var appointmentCount = await _dbContext.Appointments
                          .CountAsync(a => a.DoctorID == appointment.DoctorID && (a.AppointmentStatus == "Scheduled" || a.AppointmentStatus == "Completed")
                                           && a.AppointmentDate.Date == appointment.AppointmentDate.Date);

            // If the doctor has 10 or more appointments, throw an exception or handle it
            if (appointmentCount >= 10)
            {
                throw new InvalidOperationException("Doctor has reached the appointment limit for this day.");
            }

            var existingAppointment = await _dbContext.Appointments
                                  .FirstOrDefaultAsync(a => a.DoctorID == appointment.DoctorID
                                  && a.AppointmentStatus == appointment.AppointmentStatus
                                  && a.AppointmentDate == appointment.AppointmentDate
                                  && a.PatientID == appointment.PatientID);

            // If an appointment exists, throw an exception or handle it as per your requirements
            if (existingAppointment != null)
            {
                throw new InvalidOperationException("An appointment already exists for this time slot.");
            }


            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();

            var appointmentDetails = await (from a in _dbContext.Appointments
                                            join p in _dbContext.Patients
                                            on a.PatientID equals p.PatientID
                                            join d in _dbContext.Doctors
                                            on a.DoctorID equals d.DoctorID
                                            where a.PatientID == appointment.PatientID
                                            && a.AppointmentDate == appointment.AppointmentDate
                                            select new
                                            {
                                                PatientName = p.FirstName,
                                                PatientEmail = p.Email,
                                                DoctorName = d.DoctorName,
                                                AppointmentDate = a.AppointmentDate
                                            }).FirstOrDefaultAsync();

            var subject = "Appointment Confirmation";
            TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(appointmentDetails.AppointmentDate, indianZone);
            var formattedDate = indianTime.ToString("dd-MM-yyyy");

            var body = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            background-color: #fff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }}
        h2 {{
            color: #007BFF;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Appointment Confirmation</h2>
        <p>Dear <strong>{appointmentDetails.PatientName}</strong>,</p>
        <p>Your appointment with <strong>Dr. {appointmentDetails.DoctorName}</strong> is scheduled for <strong>{formattedDate}</strong>.</p>
        <p>Please arrive between <strong>10 AM to 1 PM</strong> or <strong>3 PM to 5 PM</strong>.</p>
        <p>We look forward to seeing you!</p>
        <p>Best regards,<br>Prathik's Clinic</p>
    </div>
    <div class='footer'>
        <p>For any queries, feel free to contact us at <a href='mailto:prathikbalaji2003@gmail.com'>prathikbalaji2003@gmail.com</a>.</p>
    </div>
</body>
</html>
";


            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("prathikbalaji2003@gmail.com", "llaw oazi sldh koxh");
            smtpServer.EnableSsl = true;
            mail.IsBodyHtml = true;

            mail.From = new MailAddress("prathikbalaji2003@gmail.com", "Prathik's Clinic");
            mail.To.Add(appointmentDetails.PatientEmail);
            mail.Subject = subject;
            mail.Body = body;


            smtpServer.Send(mail);

        }

        public async Task CancelAppointment(int appointmentId)
        {
            // Find the appointment by its ID
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);

            // Check if the appointment exists
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found.");
            }

            // Change the status to "Cancelled"
            appointment.AppointmentStatus = "Cancelled";

            // Save the changes to the database
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments()
        {
            var currentDate = DateTime.UtcNow;
            var chkDate = currentDate.Date;

            var istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            var istNow = TimeZoneInfo.ConvertTimeFromUtc(chkDate, istTimeZone);

            // Query to get appointments with medical record presence check
            var appointments = await _dbContext.Appointments
                .Where(a => a.AppointmentStatus == "Scheduled" ||  a.AppointmentStatus == "Completed")
                .GroupJoin(
                    _dbContext.MedicalRecords,
                    a => a.AppointmentID,
                    m => m.AppointmentID,
                    (a, medicalRecords) => new
                    {
                        Appointment = a,
                        IsReviewed = medicalRecords.Any(),
                        IsMissedToReview = a.AppointmentDate < chkDate && !medicalRecords.Any() // Add logic for missed review
                    }
                )
                .Join(
                    _dbContext.Patients,
                    a => a.Appointment.PatientID,
                    p => p.PatientID,
                    (a, p) => new
                    {
                        AppointmentId = a.Appointment.AppointmentID,
                        PatientName = p.FirstName + " " + p.LastName,
                        DateOfBirth = p.DateOfBirth,
                        AppointmentDate = a.Appointment.AppointmentDate,
                        ReasonForVisit = a.Appointment.ReasonForVisit,
                        IsReviewed = a.IsReviewed,
                        IsMissedToReview = a.IsMissedToReview
                    }
                )
                .ToListAsync();

            // Transform data and calculate age after retrieving data
            var appointmentDtos = appointments.Select(a => new AllAppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientName = a.PatientName,
                Age = CalculateAge(a.DateOfBirth, istNow),
                AppointmentDate = a.AppointmentDate,
                ReasonForVisit = a.ReasonForVisit,
                AppointmentStatus = a.IsMissedToReview
                    ? "Missed to Review"
                    : (a.IsReviewed ? "Reviewed" : "Yet to review")  // Use the missed review condition
            });

            return appointmentDtos;
        }

        private int CalculateAge(DateTime dateOfBirth, DateTime istNow)
        {
            int age = istNow.Year - dateOfBirth.Year;
            if (istNow < dateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }

        public async Task<IEnumerable<object>> GetPatientRecordsAfterReview()
        {
            var completedAppointments = (from p in _dbContext.Patients
                                         join a in _dbContext.Appointments on p.PatientID equals a.PatientID
                                         join m in _dbContext.MedicalRecords on a.AppointmentID equals m.AppointmentID
                                         where a.AppointmentStatus == "Completed" && m.IsBillGenerated == 0
                                         select new
                                         {
                                             PatientID = p.PatientID,
                                             AppointmentId = a.AppointmentID,
                                             PatientName = p.FirstName + " " + p.LastName,
                                             Age = DateTime.Now.Year - p.DateOfBirth.Year, 
                                             MedicalNotes = m.MedicalNotes,
                                             AppointmentDate = a.AppointmentDate
                                         }).ToList();

            return completedAppointments;

        }

        public async Task generateInvoice(int pid, int aid)
        {
            var appnts = await _dbContext.Appointments.FindAsync(aid);

            if (appnts == null)
            {
                throw new Exception("Appointment not found.");
            }

            var doc = await _dbContext.Doctors.FindAsync(appnts.DoctorID);

            if (doc == null)
            {
                throw new Exception("Doctor not found.");
            }

            var invoice = new InvoiceDTO
            {
                Date = DateTime.Now,  
                Amt = Convert.ToDecimal( doc.ConsultantFee ) , 
                Pid = pid, 
                Aid = aid  
            };

            var billing = new Billing
            {
                InvoiceDate = invoice.Date,  
                Amount = invoice.Amt,        
                PatientID = invoice.Pid,     
                AppointmentID = invoice.Aid  
            };

            _dbContext.Billings.Add(billing);

            await _dbContext.SaveChangesAsync();

            var med = _dbContext.MedicalRecords.FirstOrDefault(m => m.AppointmentID == aid);
            med.IsBillGenerated = 1;
            _dbContext.SaveChanges();

            var patient = _dbContext.Patients.FirstOrDefault(m => m.PatientID == pid);

            var mednotes = _dbContext.MedicalRecords.FirstOrDefault(m => m.AppointmentID == aid);

            string Pname = patient.FirstName + " " + patient.LastName;
            string notes = mednotes.MedicalNotes;
            var date = (appnts.AppointmentDate).ToString("dd-MM-yyyy"); // Correct date format
            string docName = doc.DoctorName; // Ensure proper doctor name formatting
            int amt = doc.ConsultantFee;

            // Generate the UPI payment link
            string paymentUrl = $"upi://pay?pa=massprathik-1@oksbi&pn=Prathik&am={amt}&cu=INR&tn=Payment";

            // Compose the email body with proper HTML formatting
            string mailBody = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                            background-color: #f2f4f8;
                            color: #333;
                            margin: 0;
                            padding: 20px;
                        }}
                        .container {{
                            background-color: #fff;
                            border-radius: 10px;
                            padding: 30px;
                            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
                            max-width: 600px;
                            margin: auto;
                        }}
                        h2 {{
                            color: #007BFF;
                            text-align: center;
                        }}
                        .info {{
                            margin: 15px 0;
                            padding: 10px;
                            border-left: 4px solid #007BFF;
                            background-color: #f9f9f9;
                        }}
                        a {{
                            background-color: #007BFF;
                            color: white;
                            padding: 10px 15px;
                            text-decoration: none;
                            border-radius: 5px;
                            transition: background-color 0.3s;
                        }}
                        a:hover {{
                            background-color: #0056b3;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 0.9em;
                            color: #666;
                            text-align: center;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Appointment Confirmation</h2>
        
                        <p>Dear <strong>{Pname}</strong>,</p>

                        <div class='info'>
                            <strong>Consultation Details:</strong><br/>
                            <strong>Doctor:</strong> Dr. {docName}<br/>
                            <strong>Consultation Date:</strong> {date}<br/>
                            <strong>Consultation Fee:</strong> INR {amt}<br/>
                            <strong>Medical Notes:</strong> {notes}
                        </div>

                        <p>To confirm your appointment, please proceed with the payment by clicking the button below:</p>
                        <p>
                            <a href='{paymentUrl}' target='_blank'>Pay Now</a>
                        </p>

                        <p>If you prefer, you can also copy and paste this UPI link into your payment app:<br/>
                        <strong>{paymentUrl}</strong></p>

                        <p>If you have any questions or need further assistance, feel free to reach out to us. We're here to help!</p>
                    </div>
    
                    <div class='footer'>
                        <p>Best regards,<br/>
                        Prathik's Clinic</p>
                    </div>
                </body>
                </html>
                ";


            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("prathikbalaji2003@gmail.com", "llaw oazi sldh koxh");
            smtpServer.EnableSsl = true;


            mail.From = new MailAddress("prathikbalaji2003@gmail.com", "Prathik's Clinic");
            mail.To.Add(patient.Email);
            mail.Subject = "Consultation Payment Request";
            mail.Body = mailBody ;
            mail.IsBodyHtml = true;


            smtpServer.Send(mail);
        }


        public class InvoiceDTO
        {
            public DateTime Date {  get; set; }
            public Decimal Amt {  get; set; }
            public int Pid { get; set; }
            public int Aid {  get; set; }
        }

    }
}
