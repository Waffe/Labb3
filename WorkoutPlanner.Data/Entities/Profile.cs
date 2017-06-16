using System;
using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.Data.Entities
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age => (int)DateTime.Now.Subtract(DateOfBirth).TotalDays / 365;
        public DateTime DateOfBirth { get; set; }
        public double? Weight { get; set; }
        public double? Lenght { get; set; }
        public double? BMI => Weight / (Lenght / 100 * Lenght / 100);
    }
}
