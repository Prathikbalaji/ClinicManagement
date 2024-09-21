﻿// <auto-generated />
using System;
using ClinicManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicManagement.Migrations
{
    [DbContext(typeof(ClinicManagementDBContext))]
    partial class ClinicManagementDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClinicManagement.Model.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("isDeleted")
                        .HasColumnType("int");

                    b.HasKey("AdminID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("ClinicManagement.Model.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentID"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppointmentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<string>("ReasonForVisit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestRecord")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("ClinicManagement.Model.Billing", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.HasKey("InvoiceID");

                    b.HasIndex("AppointmentID");

                    b.HasIndex("PatientID");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("ClinicManagement.Model.Doctor", b =>
                {
                    b.Property<int>("DoctorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorID"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("ConsultantFee")
                        .HasColumnType("int");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("isDeleted")
                        .HasColumnType("int");

                    b.HasKey("DoctorID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("ClinicManagement.Model.DoctorAvailability", b =>
                {
                    b.Property<int>("AvailabilityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvailabilityID"));

                    b.Property<string>("AvailabilityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AvailableDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.HasKey("AvailabilityID");

                    b.HasIndex("DoctorID");

                    b.ToTable("DoctorAvailabilities");
                });

            modelBuilder.Entity("ClinicManagement.Model.MedicalRecord", b =>
                {
                    b.Property<int>("MedicalRecordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicalRecordID"));

                    b.Property<int>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<int>("DoctorID")
                        .HasColumnType("int");

                    b.Property<int>("IsBillGenerated")
                        .HasColumnType("int");

                    b.Property<string>("MedicalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.HasKey("MedicalRecordID");

                    b.HasIndex("AppointmentID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("ClinicManagement.Model.Patient", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("isDeleted")
                        .HasColumnType("int");

                    b.HasKey("PatientID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("ClinicManagement.Model.Receptionist", b =>
                {
                    b.Property<int>("ReceptionistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceptionistID"));

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceptionistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("isDeleted")
                        .HasColumnType("int");

                    b.HasKey("ReceptionistID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Receptionists");
                });

            modelBuilder.Entity("ClinicManagement.Model.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ClinicManagement.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClinicManagement.Model.Admin", b =>
                {
                    b.HasOne("ClinicManagement.Model.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("ClinicManagement.Model.Admin", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClinicManagement.Model.Appointment", b =>
                {
                    b.HasOne("ClinicManagement.Model.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicManagement.Model.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicManagement.Model.Billing", b =>
                {
                    b.HasOne("ClinicManagement.Model.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicManagement.Model.Patient", "Patient")
                        .WithMany("Billings")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicManagement.Model.Doctor", b =>
                {
                    b.HasOne("ClinicManagement.Model.User", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("ClinicManagement.Model.Doctor", "UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClinicManagement.Model.DoctorAvailability", b =>
                {
                    b.HasOne("ClinicManagement.Model.Doctor", "Doctor")
                        .WithMany("Availabilities")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("ClinicManagement.Model.MedicalRecord", b =>
                {
                    b.HasOne("ClinicManagement.Model.Appointment", "Appointment")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicManagement.Model.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ClinicManagement.Model.Patient", "Patient")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicManagement.Model.Receptionist", b =>
                {
                    b.HasOne("ClinicManagement.Model.User", "User")
                        .WithOne("Receptionist")
                        .HasForeignKey("ClinicManagement.Model.Receptionist", "UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClinicManagement.Model.User", b =>
                {
                    b.HasOne("ClinicManagement.Model.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ClinicManagement.Model.Appointment", b =>
                {
                    b.Navigation("MedicalRecords");
                });

            modelBuilder.Entity("ClinicManagement.Model.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Availabilities");
                });

            modelBuilder.Entity("ClinicManagement.Model.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Billings");

                    b.Navigation("MedicalRecords");
                });

            modelBuilder.Entity("ClinicManagement.Model.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ClinicManagement.Model.User", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Doctor");

                    b.Navigation("Receptionist");
                });
#pragma warning restore 612, 618
        }
    }
}
