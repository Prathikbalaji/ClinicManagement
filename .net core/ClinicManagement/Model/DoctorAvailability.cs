using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class DoctorAvailability
    {
        [Key]
        public int AvailabilityID { get; set; }

        [Required]
        public string? AvailabilityStatus { get; set; }

        [Required]
        public DateTime AvailableDate { get; set; }

        // Foreign key to Doctor
        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }

        // Navigation property
        public Doctor? Doctor { get; set; }
    }
}
