using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models.AuthProfileViewModels
{
    public class WorkoutAuthViewModel
    {
        public Workout Workout { get; set; }

        public double? AverageRating
        {
            get
            {
                if (WorkoutRatings.Any())
                {
                    return WorkoutRatings.Average(x => x.Rate);
                }
                return null;
            }
        }

        public bool IsAuthor { get; set; }
        public IEnumerable<WorkoutRating> WorkoutRatings { get; set; }
    }
}
