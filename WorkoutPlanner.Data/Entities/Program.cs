using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Data.Entities.EfManyToMany;

namespace WorkoutPlanner.Data.Entities
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
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }
    }
}
