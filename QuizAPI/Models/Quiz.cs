namespace QuizAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int TotalMarks { get; set; }
        public string Description { get; set; } = "";
        public int QuestionSetCount { get; set; } = 10;
        public int TotalDuration { get; set; } = 10; // in minute

    }
}
