using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class removePresvription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billings_Appointments_AppointmentID",
                table: "Billings");

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

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PrescriptionID",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PrescriptionID",
                table: "MedicalRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Appointments_AppointmentID",
                table: "Billings",
                column: "AppointmentID",
                principalTable: "Appointments",
                principalColumn: "AppointmentID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Billings_Appointments_AppointmentID",
                table: "Billings");

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
                name: "PrescriptionID",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicinePrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineID);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentID = table.Column<int>(type: "int", nullable: false),
                    MedicineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_Appointments_AppointmentID",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID");
                    table.ForeignKey(
                        name: "FK_Prescription_Medicine_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicine",
                        principalColumn: "MedicineID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PrescriptionID",
                table: "MedicalRecords",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_AppointmentID",
                table: "Prescription",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_MedicineID",
                table: "Prescription",
                column: "MedicineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Appointments_AppointmentID",
                table: "Billings",
                column: "AppointmentID",
                principalTable: "Appointments",
                principalColumn: "AppointmentID");

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
