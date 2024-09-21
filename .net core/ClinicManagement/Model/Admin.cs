using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        public string? AdminName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Email { get; set; }

        public int isDeleted { get; set; } = 0;

        // Foreign key to User
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
