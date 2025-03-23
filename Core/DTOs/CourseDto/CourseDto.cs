namespace Core.Dtos
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
    }
}