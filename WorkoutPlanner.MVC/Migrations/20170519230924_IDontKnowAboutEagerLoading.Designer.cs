using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WorkoutPlanner.MVC.Data;
using WorkoutPlanner.MVC.Models;

namespace WorkoutPlanner.MVC.Migrations
{
    [DbContext(typeof(WorkoutPlannerContext))]
    [Migration("20170519230924_IDontKnowAboutEagerLoading")]
    partial class IDontKnowAboutEagerLoading
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.EfManyToMany.WorkoutExercises", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("WorkoutId");

                    b.HasKey("ExerciseId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutExercises");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Instructions");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Video");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<double?>("Lenght");

                    b.Property<string>("Name");

                    b.Property<double?>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Description");

                    b.Property<int>("Difficulty");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.ProgramRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<string>("Comment");

                    b.Property<int>("ProgramId");

                    b.Property<int>("Rate");

                    b.HasKey("Id");

                    b.HasAlternateKey("AuthorId", "ProgramId")
                        .HasName("U_ProgramRating_ProgAuthor");

                    b.HasIndex("ProgramId");

                    b.ToTable("ProgramRatings");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.WorkoutPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<int?>("Minutes");

                    b.Property<int>("ProgramId");

                    b.Property<int>("Reps");

                    b.Property<int>("Sets");

                    b.Property<int>("Week");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId")
                        .IsUnique();

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.WorkoutRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<string>("Comment");

                    b.Property<int>("Rate");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasAlternateKey("AuthorId", "WorkoutId")
                        .HasName("U_WorkoutRating_WorkAuthor");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutRatings");
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.EfManyToMany.WorkoutExercises", b =>
                {
                    b.HasOne("WorkoutPlanner.MVC.Models.Exercise", "Exercise")
                        .WithMany("Workouts")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutPlanner.MVC.Models.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.ProgramRating", b =>
                {
                    b.HasOne("WorkoutPlanner.MVC.Models.Program", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.WorkoutPlan", b =>
                {
                    b.HasOne("WorkoutPlanner.MVC.Models.Program", "Program")
                        .WithOne("WorkoutPlan")
                        .HasForeignKey("WorkoutPlanner.MVC.Models.WorkoutPlan", "ProgramId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutPlanner.MVC.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlanner.MVC.Models.WorkoutRating", b =>
                {
                    b.HasOne("WorkoutPlanner.MVC.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
