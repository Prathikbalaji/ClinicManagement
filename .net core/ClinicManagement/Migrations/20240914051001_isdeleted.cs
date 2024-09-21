using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class isdeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billings_Patients_PatientID",
                table: "Billings");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Doctors_DoctorID",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_PatientID",
                table: "MedicalRecords");

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Receptionists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Patients_PatientID",
                table: "Billings",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Doctors_DoctorID",
                table: "MedicalRecords",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientID",
                table: "MedicalRecords",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billings_Patients_PatientID",
                table: "Billings");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Doctors_DoctorID",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_PatientID",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Admins");

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Patients_PatientID",
                table: "Billings",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Doctors_DoctorID",
                table: "MedicalRecords",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientID",
                table: "MedicalRecords",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
