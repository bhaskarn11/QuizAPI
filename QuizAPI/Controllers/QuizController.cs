using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using QuizAPI.Schemas.Quiz;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "TEACHER, ADMIN")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        private void GetRecords()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = (args) => args.Header.ToLower()
            };

            using (var reader = new StreamReader(@"D:\CSharp Workspace\QuizAPI\QuizAPI\files\Book1.csv"))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<QuestionReader>();

                    var group = records.GroupBy(r => r.OptionGroupId);
                    List<Question> qs = new();
                    foreach (var rows in group)
                    {

                        
                        Question q = new Question() { 
                            Content= rows.First().QuestionContent,
                            QuestionType = (QuestionType)Enum.Parse(typeof(QuestionType), rows.First().QuestionType!) 
                        };

                        qs.Add(q);

                        foreach (var item in rows)
                        {
                            
                        }
                    }
                }
            }
        }
        
        [HttpPost("AddQuestion")]
        public Response<List<Question>?> Post(List<CreateQuestion> questions)
        {
            try
            {
                var qs = quizService.AddQuestions(questions);
                return new Response<List<Question>?>(qs, true);
            }
            catch (Exception e)
            {
                return new Response<List<Question>?>(null, false, e.Message);
            }
            
        }

        
        [HttpDelete("Question/{id}")]
        public Response<int?> Delete(int id)
        {
            try
            {
                var r = quizService.DeleteQuestion(id);
                return new Response<int?>(r, true);
            }
            catch (Exception e)
            {
                return new Response<int?>(null, false, e.Message);
            }
        }
    }
}
