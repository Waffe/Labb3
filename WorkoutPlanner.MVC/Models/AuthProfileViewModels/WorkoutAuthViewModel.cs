﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models.AuthProfileViewModels
{
    public class WorkoutAuthViewModel
    {
        public Workout Workout { get; set; }
        public bool IsAuthor { get; set; }
    }
}
