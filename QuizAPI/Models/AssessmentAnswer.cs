namespace QuizAPI.Models
{
	public class AssessmentAnswer
	{
		public int Id { get; set; }

        
        public Option? SubmittedAnswer { get; set; }

        public int QuestionId { get; set; }
        public Question? Question { get; set; }

		public int? AssessmentId { get; set; } = null;
        
    }
}
