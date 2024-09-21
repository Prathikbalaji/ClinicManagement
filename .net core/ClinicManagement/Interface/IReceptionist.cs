using ClinicManagement.Model;

namespace ClinicManagement.Interface
{

    public class DoctorDto
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
    }

    public class PatientDTO
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string MedicalNotes { get; set; }
        public DateTime Date { get; set;}
    }

    public interface IReceptionist
    {

        Task<IEnumerable<Patient>> GetPatients();

        Task<Patient> GetPatientById(int id);

        Task AddPatient(Patient patient);

        Task UpdatePatient(int id , Patient patient);

        Task DeletePatient(int id);

        Task<IEnumerable<string>> GetSpecilizations();

        Task<IEnumerable<DoctorDto>> GetDoctorsBySpecialization(string specialization);

        Task<IEnumerable<DateTime>> GetAvailDatesByDoctor(int doc);

        Task ScheduleAppointment(Appointment appointment);

        Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments();

        Task CancelAppointment(int appointmentId);

         Task<IEnumerable<object>> GetPatientRecordsAfterReview();

         Task generateInvoice(int pid, int aid);
    }
}
