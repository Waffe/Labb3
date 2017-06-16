using System.ComponentModel.DataAnnotations;

namespace WorkoutPlanner.MVC.Models
{
    public class Program
    {        
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
    }
}
