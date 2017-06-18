using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models.AuthProfileViewModels
{
    public class ProfileAuthViewModel
    {
        public Profile Profile { get; set; }
        public bool IsAuthor { get; set; }
        public IEnumerable<Workout> Workouts { get; set; }
    }
}
