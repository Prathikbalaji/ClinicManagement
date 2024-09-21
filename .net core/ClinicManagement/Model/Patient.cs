using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? Gender { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public int isDeleted { get; set; } = 0;

        // Navigation properties
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
        public ICollection<Billing>? Billings { get; set; }
    }
}
