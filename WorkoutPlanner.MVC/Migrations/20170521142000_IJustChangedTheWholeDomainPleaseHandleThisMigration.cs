using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.MVC.Migrations
{
    public partial class IJustChangedTheWholeDomainPleaseHandleThisMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "U_ProgramRating_ProgAuthor",
                table: "ProgramRatings");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "WorkoutRatings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "ProgramRatings");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "WorkoutRatings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "ProgramRatings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "U_ProgramRating_ProgAuthor",
                table: "ProgramRatings",
                columns: new[] { "ProfileId", "ProgramId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutRatings_ProfileId",
                table: "WorkoutRatings",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ProfileId",
                table: "Workouts",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProfileId",
                table: "Programs",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ProfileId",
                table: "Exercises",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Profiles_ProfileId",
                table: "Exercises",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Profiles_ProfileId",
                table: "Programs",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramRatings_Profiles_ProfileId",
                table: "ProgramRatings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Profiles_ProfileId",
                table: "Workouts",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutRatings_Profiles_ProfileId",
                table: "WorkoutRatings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Profiles_ProfileId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Profiles_ProfileId",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramRatings_Profiles_ProfileId",
                table: "ProgramRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Profiles_ProfileId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutRatings_Profiles_ProfileId",
                table: "WorkoutRatings");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutRatings_ProfileId",
                table: "WorkoutRatings");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ProfileId",
                table: "Workouts");

            migrationBuilder.DropUniqueConstraint(
                name: "U_ProgramRating_ProgAuthor",
                table: "ProgramRatings");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ProfileId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ProfileId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "WorkoutRatings");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "ProgramRatings");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "WorkoutRatings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "ProgramRatings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "U_ProgramRating_ProgAuthor",
                table: "ProgramRatings",
                columns: new[] { "AuthorId", "ProgramId" });
        }
    }
}
