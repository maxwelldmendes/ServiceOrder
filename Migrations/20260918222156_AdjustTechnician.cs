using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceOrderManager.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTechnician : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Technician");

            migrationBuilder.RenameColumn(
                name: "Skils",
                table: "Technician",
                newName: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Technician",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Technician_UserId",
                table: "Technician",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_AspNetUsers_UserId",
                table: "Technician",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technician_AspNetUsers_UserId",
                table: "Technician");

            migrationBuilder.DropIndex(
                name: "IX_Technician_UserId",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Technician");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "Technician",
                newName: "Skils");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Technician",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
