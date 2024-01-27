using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTickets.DAL.Data.Migrations
{
    public partial class RenameColumnMovieIdInTbActors_Movies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_Movies_MoviesId",
                table: "Actors_Movies");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "Actors_Movies",
                newName: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_Movies_MovieId",
                table: "Actors_Movies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_Movies_MovieId",
                table: "Actors_Movies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Actors_Movies",
                newName: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_Movies_MoviesId",
                table: "Actors_Movies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
