using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutPlanner.MVC.Models.EfManyToMany;

namespace WorkoutPlanner.MVC.Models
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
