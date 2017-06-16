using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class RemovedUniqueBetweenWorkoutsAndExercises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WorkoutExercises",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkoutExercises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises",
                columns: new[] { "ExerciseId", "WorkoutId" });
        }
    }
}
