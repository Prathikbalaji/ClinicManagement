using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Model;

namespace ClinicManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ClinicManagementDBContext _context;

        public AdminsController(ClinicManagementDBContext context)
        {
            _context = context;
        }



        [HttpGet("GetDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.Where(doc => doc.isDeleted == 0).ToListAsync();
        }

        [HttpGet("GetReceptionists")]
        public async Task<ActionResult<IEnumerable<Receptionist>>> GetReceptionists()
        {
            return await _context.Receptionists.Where(rec => rec.isDeleted == 0).ToListAsync();
        }


        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doc)
        {
            var existingDoc = await _context.Doctors.FindAsync(id);
            if (existingDoc == null)
            {
                return NotFound();
            }
            existingDoc.DoctorName = doc.DoctorName;
            existingDoc.Email = doc.Email;
            existingDoc.PhoneNumber = doc.PhoneNumber;
            existingDoc.Age = doc.Age;
            existingDoc.Specialization = doc.Specialization;
            existingDoc.ConsultantFee = doc.ConsultantFee;

            await _context.SaveChangesAsync();

            return Ok(existingDoc);
        }

        [HttpPut("UpdateReceptionist/{id}")]
        public async Task<IActionResult> UpdateReceptionist(int id, [FromBody] Receptionist rec)
        {
            var existingRec = await _context.Receptionists.FindAsync(id);
            if (existingRec == null)
            {
                return NotFound();
            }
            existingRec.ReceptionistName = rec.ReceptionistName;
            existingRec.PhoneNumber = rec.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok(existingRec);
        }


        [HttpGet("GetDoctorById/{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);

            if (doc == null)
            {
                return NotFound();
            }

            return doc;
        }

        [HttpGet("GetReceptionistById/{id}")]
        public async Task<ActionResult<Receptionist>> GetReceptionistById(int id)
        {
            var rec = await _context.Receptionists.FindAsync(id);

            if (rec == null)
            {
                return NotFound();
            }

            return rec;
        }


        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDt model)
        {
            // Add the user data
            var userEntity = new User
            {
                UserName = model.userName,
                Password = model.password,
                RoleID = model.roleID
            };

            var usrChk = _context.Users.FirstOrDefault(a => a.UserName == userEntity.UserName);

            if(usrChk != null)
            {
                return BadRequest(new { message = "Username already exists!" });
            }

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            // Add the doctor data with the user ID linked
            var doctorEntity = new Doctor
            {
                DoctorName = model.doctorName,
                Age = model.age,
                Specialization = model.specialization,
                Email = model.email,
                PhoneNumber = model.phoneNumber,
                ConsultantFee = model.consultantFee,
                UserID = userEntity.UserID
            };

            await _context.Doctors.AddAsync(doctorEntity);
            await _context.SaveChangesAsync();

            // Return created doctor with the generated ID
            return Ok();
        }

        [HttpGet("AppointmentReports/{range}/{fromDate}/{toDate}")]
        public async Task<ActionResult<object>> getAppointmentReports(string range , string fromDate , string toDate)
        {

            if (range == "thisMonth")
            {
                DateTime currentDate = DateTime.Now;
                DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                var report = (from appointment in _context.Appointments
                          join patient in _context.Patients
                            on appointment.PatientID equals patient.PatientID
                          join doctor in _context.Doctors
                            on appointment.DoctorID equals doctor.DoctorID
                          where patient.isDeleted == 0 && doctor.isDeleted == 0 && 
                          (appointment.AppointmentDate >= startDate && appointment.AppointmentDate <= endDate)
                          select new
                          {
                              AppointmentID = appointment.AppointmentID,
                              AppointmentDate = appointment.AppointmentDate,
                              AppointmentStatus = appointment.AppointmentStatus,
                              ReasonForVisit = appointment.ReasonForVisit,
                              PatientName = patient.FirstName + " " + patient.LastName,
                              PatientDOB = patient.DateOfBirth,
                              PatientGender = patient.Gender,
                              DoctorName = doctor.DoctorName,
                              DoctorSpecialization = doctor.Specialization,
                              DoctorConsultationFee = doctor.ConsultantFee,
                          }).ToList();
            return Ok(report);
            }
            else if (range == "thisWeek")
            {
                DateTime currentDate = DateTime.Now;
                int currentDayOfWeek = (int)currentDate.DayOfWeek;
                int daysToMonday = (currentDayOfWeek == 0) ? 6 : currentDayOfWeek - 1;
                DateTime startDate = currentDate.AddDays(-daysToMonday);
                DateTime endDate = startDate.AddDays(6);

                var report = (from appointment in _context.Appointments
                              join patient in _context.Patients
                                on appointment.PatientID equals patient.PatientID
                              join doctor in _context.Doctors
                                on appointment.DoctorID equals doctor.DoctorID
                              where patient.isDeleted == 0 && doctor.isDeleted == 0 &&
                              (appointment.AppointmentDate >= startDate && appointment.AppointmentDate <= endDate)
                              select new
                              {
                                  AppointmentID = appointment.AppointmentID,
                                  AppointmentDate = appointment.AppointmentDate,
                                  AppointmentStatus = appointment.AppointmentStatus,
                                  ReasonForVisit = appointment.ReasonForVisit,
                                  PatientName = patient.FirstName + " " + patient.LastName,
                                  PatientDOB = patient.DateOfBirth,
                                  PatientGender = patient.Gender,
                                  DoctorName = doctor.DoctorName,
                                  DoctorSpecialization = doctor.Specialization,
                                  DoctorConsultationFee = doctor.ConsultantFee,
                              }).ToList();
                return Ok(report);

            }

            else if(range == "fromTo")
            {
                var report = (from appointment in _context.Appointments
                              join patient in _context.Patients
                                on appointment.PatientID equals patient.PatientID
                              join doctor in _context.Doctors
                                on appointment.DoctorID equals doctor.DoctorID
                              where patient.isDeleted == 0 && doctor.isDeleted == 0 &&
                              (appointment.AppointmentDate >= Convert.ToDateTime( fromDate ) && appointment.AppointmentDate <= Convert.ToDateTime(toDate) )
                              select new
                              {
                                  AppointmentID = appointment.AppointmentID,
                                  AppointmentDate = appointment.AppointmentDate,
                                  AppointmentStatus = appointment.AppointmentStatus,
                                  ReasonForVisit = appointment.ReasonForVisit,
                                  PatientName = patient.FirstName + " " + patient.LastName,
                                  PatientDOB = patient.DateOfBirth,
                                  PatientGender = patient.Gender,
                                  DoctorName = doctor.DoctorName,
                                  DoctorSpecialization = doctor.Specialization,
                                  DoctorConsultationFee = doctor.ConsultantFee,
                              }).ToList();

                return Ok(report);
            }


            return Ok();

        }

        [HttpGet("getTotalFees")]
        public async Task<ActionResult<object>> getTotalFees()
        {
            var totalFeesByDoctor = (from appointment in _context.Appointments
                                     join doctor in _context.Doctors
                                     on appointment.DoctorID equals doctor.DoctorID
                                     where appointment.AppointmentStatus == "Completed" // Filter for completed appointments
                                     group doctor by new { doctor.DoctorID, doctor.DoctorName } into doctorGroup
                                     select new
                                     {
                                         DoctorID = doctorGroup.Key.DoctorID,
                                         DoctorName = doctorGroup.Key.DoctorName,
                                         TotalFeesCollected = doctorGroup.Count() * doctorGroup.First().ConsultantFee
                                     }).ToList();

            return Ok(totalFeesByDoctor);

        }


        [HttpPost("AddReceptionist")]
        public async Task<IActionResult> AddReceptionist([FromBody] ReceptionistDT model)
        {
            var userEntity = new User
            {
                UserName = model.userName,
                Password = model.password,
                RoleID = model.roleID
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            // Add the doctor data with the user ID linked
            var recEntity = new Receptionist
            {
                ReceptionistName = model.recName,
                PhoneNumber = model.phoneNumber,
                UserID = userEntity.UserID
            };

            await _context.Receptionists.AddAsync(recEntity);
            await _context.SaveChangesAsync();

            // Return created doctor with the generated ID
            return Ok();
        }
    }

    public class DoctorDt
    {
        public string userName { get; set; }
        public string password { get; set; }
        public int roleID { get; set; }
        public string doctorName { get; set; }
        public int age { get; set; }
        public string specialization { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int consultantFee { get; set; }
    }

    public class ReceptionistDT
    {
        public string userName { get; set; }
        public string password { get; set; }
        public int roleID { get; set; }
        public string recName { get; set; }
        public string phoneNumber { get; set; }
    }

    

}
