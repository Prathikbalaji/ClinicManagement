using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ClinicManagement.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        // Foreign key to Role
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]

        // Navigation property
        public Role? Role { get; set; }

        // One User can be associated with one Receptionist or one Doctor
        public Receptionist? Receptionist { get; set; }
        public Doctor? Doctor { get; set; }

        public Admin? Admin { get; set; }


    }
}
