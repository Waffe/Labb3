using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data.Entities;
using WorkoutPlanner.Data.Entities.EfManyToMany;

namespace WorkoutPlanner.Data.Data
{
    public interface IWorkoutPlannerContext
    {
        DbSet<Exercise> Exercises { get; set; }
        DbSet<WorkoutRating> WorkoutRatings { get; set; }
        DbSet<Workout> Workouts { get; set; }
        DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        DbSet<Profile> Profiles { get; set; }
        DbSet<Program> Programs { get; set; }
        DbSet<ProgramRating> ProgramRatings { get; set; }
    }

    public class WorkoutPlannerContext : DbContext, IWorkoutPlannerContext
    {

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercises> WorkoutExercises { get; set; }
        public DbSet<WorkoutRating> WorkoutRatings { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ProgramRating> ProgramRatings { get; set; }
        public DbSet<ExerciseRating> ExerciseRatings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=tcp:labb3.database.windows.net,1433;Initial Catalog=Labb3;Persist Security Info=False;User ID=waffe;Password=Ytg5si6!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Program>().HasKey(x => x.Id);

            modelBuilder.Entity<ProgramRating>().HasAlternateKey(wr => new {wr.ProfileId, wr.ProgramId })
                .HasName("U_ProgramRating_ProgAuthor");
           
        }
    }
}
