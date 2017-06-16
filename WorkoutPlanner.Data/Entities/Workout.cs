using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Data.Entities.EfManyToMany;

namespace WorkoutPlanner.Data.Entities
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Profile Profile { get; set; }
        public int? ProfileId { get; set; }
        public ICollection<WorkoutExercises> Exercises { get; set; }
    }
}
