using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.Schemas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        

        public AssessmentController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


       /* [HttpGet]
        public IEnumerable<Assessment> GetAllAssementByQuiz(int assessmentId)
        {
            return context.Assessments.Where(p => p.Id == assessmentId).ToList();
        }*/

        
        [HttpGet("{id}")]
        public Assessment? GetAssesmentById(int id)
        {
            Assessment? assesment = context.Assessments.Where(p => p.Id == id).FirstOrDefault();
            if (assesment == null)
            {
                throw new Exception("No assessment found");
            }
            List<AssessmentAnswer> assAns =  context.AssessmentAnswers.Where(p => p.AssessmentId == assesment.Id).Include("Question.Options").ToList();
            assesment.AssessmentAnswers = assAns;
            return assesment;
        }

        
        [HttpPost("InitiateAssessment")]
        public Assessment InitiateAssessment(int userId)
        {
            
            List<Question> questions = context.Questions.FromSql($"SELECT TOP 10 * FROM dbo.Questions ORDER BY NEWID()").Include("Options").ToList();
            DateTime startTime = DateTime.UtcNow;
            //TimeSpan duration = TimeSpan.Ad
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


        [HttpPost("Submit")]
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

        [HttpPost("SetAnswer")]
        public Response<string> SetAnswer(int assessmentId, int questionId, int optionId)
        {
            AssessmentAnswer? assessmentAnswer = context.AssessmentAnswers.
                            Where(p => p.AssessmentId == assessmentId && p.QuestionId == questionId).Include("Question.Options").FirstOrDefault();
            

            if (assessmentAnswer != null && assessmentAnswer.Question != null)
            {
                Option? op = assessmentAnswer.Question!.Options!.First(o => o.Id == optionId);
                assessmentAnswer.SubmittedAnswer = op;

                context.SaveChanges();
                return new Response<string>("", true);
            }

            throw new Exception("Assessment Error");

        }



    }
}
