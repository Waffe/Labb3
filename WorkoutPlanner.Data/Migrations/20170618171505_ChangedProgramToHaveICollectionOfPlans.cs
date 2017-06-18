using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.Data.Migrations
{
    public partial class ChangedProgramToHaveICollectionOfPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans",
                column: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_ProgramId",
                table: "WorkoutPlans",
                column: "ProgramId",
                unique: true);
        }
    }
}
