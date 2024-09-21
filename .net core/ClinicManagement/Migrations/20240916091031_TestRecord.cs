using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class TestRecord : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Prescription_PrescriptionID",
                table: "MedicalRecords");

            migrationBuilder.AddColumn<int>(
                name: "ConsultantFee",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TestRecord",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Prescription_PrescriptionID",
                table: "MedicalRecords",
                column: "PrescriptionID",
                principalTable: "Prescription",
                principalColumn: "PrescriptionId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Prescription_PrescriptionID",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "ConsultantFee",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "TestRecord",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Patients_PatientID",
                table: "Billings",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Doctors_DoctorID",
                table: "MedicalRecords",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientID",
                table: "MedicalRecords",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Prescription_PrescriptionID",
                table: "MedicalRecords",
                column: "PrescriptionID",
                principalTable: "Prescription",
                principalColumn: "PrescriptionId");
        }
    }
}
