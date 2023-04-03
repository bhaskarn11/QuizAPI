namespace QuizAPI.Schemas
{
    public class QuestionReader
    {
        public string? QuestionContent { get; set; }
        public string? OptionGroupId { get; set; }
        public int IsRightOption { get; set; }
        public string? QuestionType { get; set; }
    }
}
