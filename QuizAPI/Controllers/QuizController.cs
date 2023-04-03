using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Microsoft.Extensions.Options;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public QuizController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
        public List<Question> Post(List<CreateQuestion> questions)
        {
            List<Question> qs = new();
            foreach (var question in questions)
            {

                List<Option> ops = new();
                foreach(var option in question.Options)
                {
                    Option? op = mapper.Map<Option>(option);
                    
                    ops.Add(op);
                }

                Question q = new()
                {
                    Content = question.Content,
                    Point = question.Point,
                    QuestionType = question.QuestionType,
                    Options = ops
                };
                
                context.AddRange(q);
                qs.Add(q);
            }

            context.SaveChanges();

           

            // context.BulkInsert(qs);
            return qs;
            
            
        }

        
        [HttpDelete("Question/{id}")]
        public int Delete(int id)
        {
            Question? q = context.Questions.Where(q => q.Id == id).FirstOrDefault();
            if (q == null)
            {
                throw new Exception("Error");
            }

            context.Remove(q);
            return context.SaveChanges();  
        }
    }
}
