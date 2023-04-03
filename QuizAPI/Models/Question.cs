namespace QuizAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public List<Option>? Options { get; set; } = new List<Option>();
        
        public QuestionType QuestionType { get; set; }
        public int Point { get; set; }
    }

    public enum QuestionType
    {
        TEXT,
        AUDIO,
        VISUAL
    }

}
