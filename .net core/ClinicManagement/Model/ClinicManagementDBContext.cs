using Microsoft.EntityFrameworkCore;
using ClinicManagement.Model;

namespace ClinicManagement.Model
{
    public class ClinicManagementDBContext: DbContext
    {

        public ClinicManagementDBContext(DbContextOptions<ClinicManagementDBContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Admin> Admins { get; set; }

    }
}
