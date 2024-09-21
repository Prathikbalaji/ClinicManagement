using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Model
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string? RoleName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
