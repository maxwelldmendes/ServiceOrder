using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceOrderManager.Migrations
{
    /// <inheritdoc />
    public partial class Ajustesdetabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Client_ClientId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Technician_AssignedTechnicianId",
                table: "ServiceAppointments");

            migrationBuilder.RenameColumn(
                name: "PrimeryEmail",
                table: "Client",
                newName: "PrimaryEmail");

            migrationBuilder.RenameColumn(
                name: "PrimariPhone",
                table: "Client",
                newName: "PrimaryPhone");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ServiceAppointments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ServiceAppointments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Client_ClientId",
                table: "ServiceAppointments",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Technician_AssignedTechnicianId",
                table: "ServiceAppointments",
                column: "AssignedTechnicianId",
                principalTable: "Technician",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Client_ClientId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Technician_AssignedTechnicianId",
                table: "ServiceAppointments");

            migrationBuilder.RenameColumn(
                name: "PrimaryPhone",
                table: "Client",
                newName: "PrimariPhone");

            migrationBuilder.RenameColumn(
                name: "PrimaryEmail",
                table: "Client",
                newName: "PrimeryEmail");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ServiceAppointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ServiceAppointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Client_ClientId",
                table: "ServiceAppointments",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Technician_AssignedTechnicianId",
                table: "ServiceAppointments",
                column: "AssignedTechnicianId",
                principalTable: "Technician",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
