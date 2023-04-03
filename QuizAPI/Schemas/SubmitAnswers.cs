namespace QuizAPI.Schemas
{
    public class SubmitAnswers
    {
        public int AssessmentId { get; set; }
        public List<Option> Answers { get; set; } = new();
    }
}
