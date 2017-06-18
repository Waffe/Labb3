using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WorkoutPlanner.Data.Data;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.Data.Migrations
{
    [DbContext(typeof(WorkoutPlannerContext))]
    [Migration("20170618121715_AddedRegDateToProfile")]
    partial class AddedRegDateToProfile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.EfManyToMany.WorkoutExercises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseId");

                    b.Property<int?>("Minutes");

                    b.Property<int>("Reps");

                    b.Property<int>("Sets");

                    b.Property<string>("Weight");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutExercises");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Instructions");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ProfileId");

                    b.Property<string>("Video");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<double?>("Lenght");

                    b.Property<string>("Name");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<double?>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("Difficulty");

                    b.Property<string>("Name");

                    b.Property<int?>("ProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.ProgramRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int?>("ProfileId")
                        .IsRequired();

                    b.Property<int>("ProgramId");

                    b.Property<int>("Rate");

                    b.HasKey("Id");

                    b.HasAlternateKey("ProfileId", "ProgramId")
                        .HasName("U_ProgramRating_ProgAuthor");

                    b.HasIndex("ProgramId");

                    b.ToTable("ProgramRatings");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.WorkoutPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<int>("ProgramId");

                    b.Property<int>("Week");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId")
                        .IsUnique();

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.WorkoutRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int?>("ProfileId");

                    b.Property<int>("Rate");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutRatings");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.EfManyToMany.WorkoutExercises", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Exercise", "Exercise")
                        .WithMany("Workouts")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutPlanner.Data.Entities.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Exercise", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Program", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.ProgramRating", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutPlanner.Data.Entities.Program", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.Workout", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.WorkoutPlan", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Program", "Program")
                        .WithOne("WorkoutPlan")
                        .HasForeignKey("WorkoutPlanner.Data.Entities.WorkoutPlan", "ProgramId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutPlanner.Data.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlanner.Data.Entities.WorkoutRating", b =>
                {
                    b.HasOne("WorkoutPlanner.Data.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("WorkoutPlanner.Data.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
