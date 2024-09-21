using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string? AppointmentStatus { get; set; }

        public string? ReasonForVisit { get; set; }
        public string? TestRecord { get; set; }

        [ForeignKey("PatientID")]
        public int PatientID { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
        //public Billing? Billing { get; set; }
    }
}
