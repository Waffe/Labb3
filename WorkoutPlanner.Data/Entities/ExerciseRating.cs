using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutPlanner.Data.Entities
{
    public class ExerciseRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
