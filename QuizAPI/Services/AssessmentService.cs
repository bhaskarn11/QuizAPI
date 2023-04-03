using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace QuizAPI.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public AssessmentService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Assessment? GetAssesmentById(int id)
        {
            Assessment? assesment = context.Assessments.Where(p => p.Id == id).FirstOrDefault();
            if (assesment == null)
            {
                throw new Exception("No assessment found");
            }
            List<AssessmentAnswer> assAns = context.AssessmentAnswers.Where(p => p.AssessmentId == assesment.Id).Include("Question.Options").ToList();
            assesment.AssessmentAnswers = assAns;
            return assesment;
        }


        
        public Assessment InitiateAssessment(int userId)
        {

            List<Question> questions = context.Questions.FromSql($"SELECT TOP 10 * FROM dbo.Questions ORDER BY NEWID()").Include("Options").ToList();
            DateTime startTime = DateTime.UtcNow;
            
            if (questions.IsNullOrEmpty())
            {
                throw new Exception("No questions found");
            }


            List<AssessmentAnswer> assessmentAnswers = new();
            foreach (var q in questions)
            {
                AssessmentAnswer assAnswer = new() { Question = q };
                assessmentAnswers.Add(assAnswer);
            }
            Assessment assesment = new()
            {
                UserId = userId,
                StartedAt = startTime,
                AssessmentAnswers = assessmentAnswers
            };
            context.Add(assesment);

            context.SaveChanges();
            return assesment;
        }


       
        public Assessment SubmitAssessment(int assessmentId)
        {
            DateTime submittedAt = DateTime.UtcNow;
            Assessment? assessment = context.Assessments.Where(p => p.Id == assessmentId).Include("AssessmentAnswers.Question").Include("AssessmentAnswers.SubmittedAnswer").FirstOrDefault();

            if (assessment == null || assessment.AssessmentAnswers.IsNullOrEmpty())
            {
                throw new Exception("Error occured at assessment submission");
            }

            int score = 0;

            foreach (var answer in assessment.AssessmentAnswers)
            {
                if (answer != null && answer.SubmittedAnswer != null &&
                    answer.SubmittedAnswer.IsRightOption)
                {
                    score += answer.Question!.Point;
                }
            }
            assessment.Score = score;
            assessment.SubmittedAt = submittedAt;

            context.SaveChanges();
            return assessment;
        }

    
        public AssessmentAnswer? SetAnswer(int assessmentId, int questionId, int optionId)
        {
            AssessmentAnswer? assessmentAnswer = context.AssessmentAnswers.
                            Where(p => p.AssessmentId == assessmentId && p.QuestionId == questionId).Include("Question.Options").FirstOrDefault();


            if (assessmentAnswer != null && assessmentAnswer.Question != null)
            {
                Option? op = assessmentAnswer.Question!.Options!.First(o => o.Id == optionId);
                assessmentAnswer.SubmittedAnswer = op;

                context.SaveChanges();
                return assessmentAnswer;
            }

            throw new Exception("Assessment Error");

        }
    }
}
