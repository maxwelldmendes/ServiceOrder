using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceOrderManager.Migrations
{
    /// <inheritdoc />
    public partial class FinalAdjusts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechnicianId",
                table: "ServiceAppointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_TechnicianId",
                table: "ServiceAppointments",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Technician_TechnicianId",
                table: "ServiceAppointments",
                column: "TechnicianId",
                principalTable: "Technician",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Technician_TechnicianId",
                table: "ServiceAppointments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAppointments_TechnicianId",
                table: "ServiceAppointments");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "ServiceAppointments");
        }
    }
}
