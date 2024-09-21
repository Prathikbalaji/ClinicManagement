using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        public string? DoctorName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Specialization { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int ConsultantFee { get; set; }
        public int isDeleted { get; set; } = 0;

        // Foreign key to User
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        // Navigation property
        public User? User { get; set; }

        // Navigation property for availability and appointments
        public ICollection<DoctorAvailability>? Availabilities { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
