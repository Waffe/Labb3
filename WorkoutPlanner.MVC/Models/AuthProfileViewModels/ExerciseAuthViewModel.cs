using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models.AuthProfileViewModels
{
    public class ExerciseAuthViewModel
    {
        public Exercise Exercise { get; set; }

        public double? AverageRating
        {
            get
            {
                if (ExerciseRatings.Any())
                {
                    return ExerciseRatings.Average(x => x.Rate);
                }
                return null;
            }
        }

        public bool IsAuthor { get; set; }
        public IEnumerable<ExerciseRating> ExerciseRatings { get; set; }
    }
}
