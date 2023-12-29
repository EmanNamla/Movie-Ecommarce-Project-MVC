using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTickets.DAL.Data.Migrations
{
    public partial class EditDataTypeColumnProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureUrl",
                table: "Actors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
