using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models.WorkoutViewModels
{
    public class EditWorkoutViewModel
    {
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
