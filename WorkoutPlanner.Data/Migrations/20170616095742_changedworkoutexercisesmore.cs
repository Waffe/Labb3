using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class changedworkoutexercisesmore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "WorkoutExercises",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WorkoutExercises");
        }
    }
}
