using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class ChangedExerciseToHaveIcollectionOfWOrkoutPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "WorkoutPlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_ExerciseId",
                table: "WorkoutPlans",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Exercises_ExerciseId",
                table: "WorkoutPlans",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Exercises_ExerciseId",
                table: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_ExerciseId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "WorkoutPlans");
        }
    }
}
