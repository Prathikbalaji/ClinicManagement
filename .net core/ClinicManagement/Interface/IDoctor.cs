using ClinicManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Interface
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ReasonForVisit { get; set; }
        public string AppointmentStatus { get; set; }

    }

    public class AppointmentReviewDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int PatientID { get; set; }

        public int Age { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ReasonForVisit { get; set; }
        public string TestRecord { get; set; }
    }



    public class AllAppointmentDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ReasonForVisit { get; set; }
        public string AppointmentStatus { get; set; }

    }

    public interface IDoctor
    {
        Task AddAvailability(DoctorAvailability doctorAvailability);

        Task<IEnumerable<AppointmentReviewDto>> getAppointmentsById(int AppID);

        Task<IEnumerable<AppointmentDto>> GetDoctorAppointments(int DocID);
        Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments(int DocID);

        Task SaveReview(MedicalRecord med);

        Task<IEnumerable<DoctorAvailability>> GetDoctorAvailability(int doctorId);

        Task SaveDoctorAvailability(int doctorId, List<DoctorAvailability> availabilityData);

        Task DeleteDoctorAvailability(string doctorId, string date);

    }
}
