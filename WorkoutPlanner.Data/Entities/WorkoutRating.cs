using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutPlanner.Data.Entities
{
    public class WorkoutRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
