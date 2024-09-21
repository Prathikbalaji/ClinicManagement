using ClinicManagement.Interface;
using ClinicManagement.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Repository
{
    public class DoctorRepository : IDoctor
    {

        private readonly ClinicManagementDBContext _dbContext;

        public DoctorRepository(ClinicManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAvailability(DoctorAvailability doctorAvailability)
        {
            await _dbContext.DoctorAvailabilities.AddAsync(doctorAvailability);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<AppointmentDto>> GetDoctorAppointments(int DocID)
       {
            var currentDate = DateTime.UtcNow;

            var currentDateUtc = DateTime.UtcNow; // Current date and time in UTC
            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"); // Get IST time zone info
            var currentDateInIndia = TimeZoneInfo.ConvertTimeFromUtc(currentDateUtc, indianTimeZone); // Convert UTC to IST

            var chkDate = currentDateInIndia.Date; // Get the date part


            var appointments = await _dbContext.Appointments
                .Where(a => a.DoctorID == DocID && (a.AppointmentStatus == "Scheduled" || a.AppointmentStatus == "Completed") && a.AppointmentDate == chkDate)
                .Join(
                    _dbContext.Patients,
                    a => a.PatientID,
                    p => p.PatientID,
                    (a, p) => new
                    {
                        AppointmentID = a.AppointmentID,
                        PatientName = p.FirstName + " " + p.LastName,
                        AppointmentStatus = a.AppointmentStatus,
                        DateOfBirth = p.DateOfBirth,
                        AppointmentDate = a.AppointmentDate,
                        ReasonForVisit = a.ReasonForVisit
                    }
                )
                .ToListAsync();

            var appointmentDtos = appointments.Select(a => new AppointmentDto
            {
                AppointmentId = a.AppointmentID,
                PatientName = a.PatientName,
                Age = CalculateAge(a.DateOfBirth, currentDate),
                AppointmentDate = a.AppointmentDate,
                ReasonForVisit = a.ReasonForVisit,
                AppointmentStatus = a.AppointmentStatus
            });

            return appointmentDtos;
        }

        public async Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments(int docID)
        {
            var currentDate = DateTime.UtcNow;
            var chkDate = currentDate.Date;

            var istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            var istNow = TimeZoneInfo.ConvertTimeFromUtc(chkDate, istTimeZone);

            // Query to get appointments with medical record presence check
            var appointments = await _dbContext.Appointments
                .Where(a => a.DoctorID == docID && a.AppointmentStatus == "Scheduled")
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

        public async Task<IEnumerable<AppointmentReviewDto>> getAppointmentsById(int AppID)
        {
            var currentDate = DateTime.UtcNow;
            var chkDate = currentDate.Date;

            var appointments = await _dbContext.Appointments
                .Where(a => a.AppointmentID == AppID)
                .Join(
                    _dbContext.Patients,
                    a => a.PatientID,
                    p => p.PatientID,
                    (a, p) => new
                    {
                        AppointmentID = a.AppointmentID,
                        PatientName = p.FirstName + " " + p.LastName,
                        PatientID = p.PatientID,
                        DateOfBirth = p.DateOfBirth,
                        AppointmentDate = a.AppointmentDate,
                        ReasonForVisit = a.ReasonForVisit,
                        TestRecord = a.TestRecord
                    }
                )
                .ToListAsync();

            var appointmentDtos = appointments.Select(a => new AppointmentReviewDto
            {
                AppointmentId = a.AppointmentID,
                PatientName = a.PatientName,
                PatientID = a.PatientID,
                Age = CalculateAge(a.DateOfBirth, currentDate),
                AppointmentDate = a.AppointmentDate,
                ReasonForVisit = a.ReasonForVisit,
                TestRecord = a.TestRecord
            });

            return appointmentDtos;
        }

        public async Task SaveReview(MedicalRecord med)
        {
            await _dbContext.MedicalRecords.AddAsync(med);
            await _dbContext.SaveChangesAsync();

            var upd =await _dbContext.Appointments.FindAsync(med.AppointmentID);
            upd.AppointmentStatus = "Completed";
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorAvailability>> GetDoctorAvailability(int doctorId)
        {
            var availability = await _dbContext.DoctorAvailabilities
                                             .Where(a => a.DoctorID == doctorId)
                                             .ToListAsync();
            return availability;
        }

        public async Task SaveDoctorAvailability(int doctorId, List<DoctorAvailability> availabilityData)
        {
            var existingEntries = _dbContext.DoctorAvailabilities.Where(a => a.DoctorID == doctorId);

            // Save new availability data
            foreach (var availability in availabilityData)
            {
                availability.DoctorID = doctorId;
                _dbContext.DoctorAvailabilities.Add(availability);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDoctorAvailability(string doctorId, string date)
        {
            var appnts = _dbContext.DoctorAvailabilities.FirstOrDefault(a => a.DoctorID == int.Parse(doctorId) && a.AvailableDate == Convert.ToDateTime( date ));
            if(appnts == null)
            {
                throw new Exception();
            }
            _dbContext.DoctorAvailabilities.Remove(appnts);
            await _dbContext.SaveChangesAsync();
        }


    }
}
