using ClinicManagement.Interface;
using ClinicManagement.Model;
using ClinicManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("AddAvailability")]
        public async Task AddAvailability(DoctorAvailability docAvailability)
        {
            await _doctorService.AddAvailability(docAvailability);
        }

        [HttpGet("GetDoctorAppointments/{DocId}")]
        public async Task<IEnumerable<AppointmentDto>> GetDoctorAppointments(int DocId)
        {
            return await _doctorService.GetDoctorAppointments(DocId);
        }

        [HttpGet("GetAllDoctorAppointments")]
        public async Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments(int docID)
        {
            return await _doctorService.GetAllDoctorAppointments(docID);
        }

        [HttpGet("getAppointmentsById/{AppId}")]
        public async Task<IEnumerable<AppointmentReviewDto>> getAppointmentsById(int AppId)
        {
            return await _doctorService.getAppointmentsById(AppId);
        }

        [HttpPost("SaveReview")]
        public async Task<IActionResult> SaveReview(MedicalRecord med)
        {
             await _doctorService.SaveReview(med);
            return Ok();
        }

        [HttpGet("availability/{doctorId}")]
        public async Task<IEnumerable<DoctorAvailability>> GetDoctorAvailability(int doctorId)
        {
        
            return await _doctorService.GetDoctorAvailability(doctorId);
        }


        [HttpPost("availability/{doctorId}")]
        public async Task<IActionResult> SaveDoctorAvailability(int doctorId, [FromBody] List<DoctorAvailability> availabilityData)
        {
            await _doctorService.SaveDoctorAvailability(doctorId, availabilityData);
            return Ok();
        }

        [HttpDelete("deleteAvailability/{doctorId}/{date}")]
        public async Task<IActionResult> DeleteDoctorAvailability(string doctorId , string date)
        {
            await _doctorService.DeleteDoctorAvailability(doctorId, date);
            return Ok();
        }

    }
}
