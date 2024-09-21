using ClinicManagement.Interface;
using ClinicManagement.Model;

namespace ClinicManagement.Service
{
    public class ReceptionistService
    {
        private readonly IReceptionist rec;

        public ReceptionistService(IReceptionist rec)
        {
            this.rec = rec;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await rec.GetPatients();
        }

        public async Task AddPatient(Patient patient)
        {
            await rec.AddPatient(patient);
        }

        public async Task UpdatePatient(int id, Patient patient)
        {
            await rec.UpdatePatient(id, patient);
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await rec.GetPatientById(id);
        }

        public async Task<IEnumerable<string>> GetSpecilizations()
        {
            return await rec.GetSpecilizations();
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsBySpecilization(string Specialization)
        {
            return await rec.GetDoctorsBySpecialization(Specialization);
        }

        public async Task DeletePatient(int id)
        {
            await rec.DeletePatient(id);
        }

        public async Task<IEnumerable<DateTime>> GetAvailDatesByDoctor(int doc)
        {
            return await rec.GetAvailDatesByDoctor(doc);
        }

        public async Task ScheduleAppointment(Appointment appointment)
        {
            await rec.ScheduleAppointment(appointment);
        }

        public async Task CancelAppointment(int appointmentId)
        {
            await rec.CancelAppointment(appointmentId);
        }

        public async Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments()
        {
            return await rec.GetAllDoctorAppointments();
        }

        public async Task<IEnumerable<object>> GetPatientRecordsAfterReview()
        {
            return await rec.GetPatientRecordsAfterReview();
        }

        public async Task generateInvoice(int pid, int aid)
        {
            await rec.generateInvoice(pid, aid);
        }

    }
}
