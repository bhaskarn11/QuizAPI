namespace QuizAPI.Services.Interfaces
{
    public interface IAssessmentService
    {
        public Assessment? GetAssesmentById(int id);
        public Assessment InitiateAssessment(int userId);
        public Assessment SubmitAssessment(int assessmentId);
        public Response<string> SetAnswer(int assessmentId, int questionId, int optionId);

    }
}
