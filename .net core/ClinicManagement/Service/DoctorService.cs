using ClinicManagement.Interface;
using ClinicManagement.Model;

namespace ClinicManagement.Service
{
    public class DoctorService
    {

        private readonly IDoctor doc;

        public DoctorService(IDoctor doc)
        {
            this.doc = doc;
        }

        public async Task AddAvailability(DoctorAvailability doctorAvailability)
        {
            await doc.AddAvailability(doctorAvailability);
        }

        public async Task<IEnumerable<AppointmentDto>> GetDoctorAppointments(int DocID)
        {
            return await doc.GetDoctorAppointments(DocID);
        }

        public async Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments(int docID)
        {
            return await doc.GetAllDoctorAppointments(docID);
        }

        public async Task<IEnumerable<AppointmentReviewDto>> getAppointmentsById(int AppID)
        {
            return await doc.getAppointmentsById(AppID);
        }

        public async Task SaveReview(MedicalRecord med)
        {
            await doc.SaveReview(med);
        }

        public async Task<IEnumerable<DoctorAvailability>> GetDoctorAvailability(int doctorId)
        {
            return await doc.GetDoctorAvailability(doctorId);
        }

        public async Task SaveDoctorAvailability(int doctorId, List<DoctorAvailability> availabilityData)
        {
            await doc.SaveDoctorAvailability(doctorId, availabilityData);
        }

        public async Task DeleteDoctorAvailability(string doctorId, string date)
        {
            await doc.DeleteDoctorAvailability(doctorId, date);
        }

    }
}
