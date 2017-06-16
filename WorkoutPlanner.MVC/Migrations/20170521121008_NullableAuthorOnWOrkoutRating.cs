using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.MVC.Migrations
{
    public partial class NullableAuthorOnWOrkoutRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "U_WorkoutRating_WorkAuthor",
                table: "WorkoutRatings");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "WorkoutRatings",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "WorkoutRatings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "U_WorkoutRating_WorkAuthor",
                table: "WorkoutRatings",
                columns: new[] { "AuthorId", "WorkoutId" });
        }
    }
}
