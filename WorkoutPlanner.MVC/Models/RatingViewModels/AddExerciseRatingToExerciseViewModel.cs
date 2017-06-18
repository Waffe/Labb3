using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models.RatingViewModels
{
    public class AddExerciseRatingToExerciseViewModel
    {
        public Exercise Exercise { get; set; }
        public ExerciseRating ExerciseRating { get; set; }
    }
}
