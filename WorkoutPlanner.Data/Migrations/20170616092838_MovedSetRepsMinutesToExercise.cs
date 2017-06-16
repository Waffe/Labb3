using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class MovedSetRepsMinutesToExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "WorkoutPlans");

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                table: "WorkoutExercises",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "WorkoutExercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "WorkoutExercises",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "WorkoutExercises");

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                table: "WorkoutPlans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "WorkoutPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "WorkoutPlans",
                nullable: false,
                defaultValue: 0);
        }
    }
}
