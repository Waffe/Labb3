using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.MVC.Models;
using WorkoutPlanner.MVC.Models.EfManyToMany;


namespace WorkoutPlanner.MVC.Data
{
    public interface IWorkoutPlannerContext
    {
        DbSet<Exercise> Exercises { get; set; }
        DbSet<WorkoutRating> WorkoutRatings { get; set; }
        DbSet<Workout> Workouts { get; set; }
        DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        DbSet<Profile> Profiles { get; set; }
        DbSet<Models.Program> Programs { get; set; }
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
        public DbSet<Models.Program> Programs { get; set; }
        public DbSet<ProgramRating> ProgramRatings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = WorkoutPlanner; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutExercises>().HasKey(sb => new {sb.ExerciseId, sb.WorkoutId});
            modelBuilder.Entity<Models.Program>().HasKey(x => x.Id);

            modelBuilder.Entity<ProgramRating>().HasAlternateKey(wr => new {wr.ProfileId, wr.ProgramId })
                .HasName("U_ProgramRating_ProgAuthor");


            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
            modelBuilder.Ignore<ApplicationUser>();
        }
    }
}
