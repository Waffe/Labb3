using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.MVC.Migrations
{
    public partial class panicNoTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Exercises");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans",
                column: "ProgramId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
