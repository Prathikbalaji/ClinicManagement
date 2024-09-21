using ClinicManagement.Interface;
using ClinicManagement.Model;
using ClinicManagement.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {

        private readonly ReceptionistService _service;

        public ReceptionistController(ReceptionistService receptionistService)
        {
            _service = receptionistService;
        }

        // GET: api/<ReceptionistController>
        [HttpGet("GetPatients")]
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _service.GetPatients(); 
        }

        // GET api/<ReceptionistController>/5
        [HttpGet("GetPatientById/{id}")]
        public async Task<Patient> GetPatientById(int id)
        {
            return await _service.GetPatientById(id);
        }

        // POST api/<ReceptionistController>
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] Patient pat)
        {
            try
            {
                await _service.AddPatient(pat);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        // PUT api/<ReceptionistController>/5
        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            await _service.UpdatePatient(id, patient);
            return Ok();
        }

        // DELETE api/<ReceptionistController>/5
        [HttpDelete("DeletePatient/{id}")]
        public async Task DeletePatient(int id)
        {
            await _service.DeletePatient(id);
        }

        [HttpGet("GetSpecilizations")]
        public async Task<IEnumerable<string>> GetSpecilizations()
        {
            return await _service.GetSpecilizations();
        }

        [HttpGet("GetDoctorsBySpecilization")]
        public async Task<IEnumerable<DoctorDto>> GetDoctorsBySpecilization(string Specialization)
        {
            return await _service.GetDoctorsBySpecilization(Specialization);
        }

        [HttpGet("GetAvailDatesByDoctor")]
        public async Task<IEnumerable<DateTime>> GetAvailDatesByDoctor(int doc)
        {
            return await _service.GetAvailDatesByDoctor(doc);
        }

        [HttpPost("ScheduleAppointment")]
        public async Task<IActionResult> ScheduleAppointment(Appointment appointment)
        {
            try
            {
                await _service.ScheduleAppointment(appointment);
                return Ok(new { message = "Appointment scheduled successfully!" });
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(new { message = ex.Message, errorCode = "An Appointment Already Exists for this Patient on this day" });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpPut("CancelAppointment/{id}")]
        public async Task CancelAppointment(int id)
        {
            await _service.CancelAppointment(id);
        }

        [HttpGet("GetAllDoctorAppointments")]
        public async Task<IEnumerable<AllAppointmentDto>> GetAllDoctorAppointments()
        {
            return await _service.GetAllDoctorAppointments();
        }

        [HttpGet("GetPatientRecordsAfterReview")]
        public async Task<IEnumerable<object>> GetPatientRecordsAfterReview()
        {
            return await _service.GetPatientRecordsAfterReview();
        }

        [HttpPost("GenerateInvoice/{pid}/{aid}")]
        public async Task<IActionResult> generateInvoice(int pid, int aid)
        {
            await _service.generateInvoice(pid, aid);

            return Ok();
        }

    }
}
