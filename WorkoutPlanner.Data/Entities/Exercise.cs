using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutPlanner.Data.Entities.EfManyToMany;

namespace WorkoutPlanner.Data.Entities
{
    public class Exercise
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Instructions { get; set; }
        public string Video { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<WorkoutExercises> Workouts { get; set; }
    }
}
