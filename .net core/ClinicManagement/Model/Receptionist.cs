using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Receptionist
    {
        [Key]
        public int ReceptionistID { get; set; }

        [Required]
        public string? ReceptionistName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        public int isDeleted { get; set; } = 0;

        // Foreign key to User
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
