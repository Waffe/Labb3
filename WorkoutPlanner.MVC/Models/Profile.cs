using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.MVC.Models
{
    public class Profile
    {
        public Profile()
        {
            
        }
        public Profile(string userId)
        {
            ApplicationUserId = userId;
        }
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [DisplayName("User´s Name")]
        public string Name { get; set; }
        public int? Age => (int)DateTime.Now.Subtract(DateOfBirth).TotalDays / 365;
        public DateTime DateOfBirth { get; set; }
        public double? Weight { get; set; }
        public double? Lenght { get; set; }
        public double? BMI => Weight / (Lenght / 100 * Lenght / 100);

    }
}
