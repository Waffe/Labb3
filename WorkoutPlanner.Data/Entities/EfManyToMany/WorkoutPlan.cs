﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutPlanner.Data.Entities.EfManyToMany
{
    public class WorkoutPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int Week { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

    }
}
