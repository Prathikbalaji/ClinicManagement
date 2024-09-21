using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordID { get; set; }

        public string? MedicalNotes { get; set; }

        [Required]
        public int IsBillGenerated { get; set; } = 0;

        // Foreign key relationships
        [ForeignKey("PatientID")]
        public int PatientID { get; set; }

        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
