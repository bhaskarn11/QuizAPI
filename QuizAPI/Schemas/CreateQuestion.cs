namespace QuizAPI.Schemas
{
    public class CreateQuestion
    {
        public string? Content { get; set; }
        public ICollection<AddOption> Options { get; set; } = new List<AddOption>();
        
        public QuestionType QuestionType { get; set; }
        public int Point { get; set; }
    }
}
