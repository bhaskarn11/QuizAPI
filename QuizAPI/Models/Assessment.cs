namespace QuizAPI.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        /*public Quiz? Quiz { get; set; }
        public int QuizId { get; set; }*/
        public int Score { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<AssessmentAnswer> AssessmentAnswers { get; set; } = new List<AssessmentAnswer>();
    }
}
