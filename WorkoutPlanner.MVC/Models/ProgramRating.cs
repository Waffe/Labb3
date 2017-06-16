namespace WorkoutPlanner.MVC.Models
{
    public class ProgramRating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
    }
}
