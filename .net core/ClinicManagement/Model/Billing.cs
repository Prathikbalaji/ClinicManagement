using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Billing
    {
        [Key]
        public int InvoiceID { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        // Foreign key relationships
        [ForeignKey("PatientID")]
        public int PatientID { get; set; }

        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }

    }
}
