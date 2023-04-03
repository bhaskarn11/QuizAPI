namespace QuizAPI.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        
        public int QuestionId { get; set; }

        public bool IsRightOption { get; set; } = false;

    }
}
