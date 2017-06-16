namespace WorkoutPlanner.Data.Entities.EfManyToMany
{
    public class WorkoutExercises
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public string Weight { get; set; }
        public int? Minutes { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
