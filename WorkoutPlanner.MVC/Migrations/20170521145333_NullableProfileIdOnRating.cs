using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.MVC.Migrations
{
    public partial class NullableProfileIdOnRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutRatings_Profiles_ProfileId",
                table: "WorkoutRatings");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "WorkoutRatings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutRatings_Profiles_ProfileId",
                table: "WorkoutRatings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutRatings_Profiles_ProfileId",
                table: "WorkoutRatings");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "WorkoutRatings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutRatings_Profiles_ProfileId",
                table: "WorkoutRatings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
