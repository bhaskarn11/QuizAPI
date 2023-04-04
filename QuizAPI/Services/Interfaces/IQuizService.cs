using QuizAPI.Schemas.Quiz;

namespace QuizAPI.Services.Interfaces
{
    public interface IQuizService
    {
        public List<Question> AddQuestions(List<CreateQuestion> questions);
        public int DeleteQuestion(int id);
    }
}
