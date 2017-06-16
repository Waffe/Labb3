using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.MVC.Models.EfManyToMany;

namespace WorkoutPlanner.MVC.Models.WorkoutViewModels
{
    public class AddExerciseToWorkoutViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public Exercise ExerciseToAdd { get; set; }
        public ICollection<WorkoutExercises> Exercises { get; set; }
    }
}
