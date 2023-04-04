using AutoMapper;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using QuizAPI.Schemas.Quiz;

namespace QuizAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public QuizService(AppDbContext context, IMapper mapper)
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


                        Question q = new Question()
                        {
                            Content = rows.First().QuestionContent,
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

        
        public List<Question> AddQuestions(List<CreateQuestion> questions)
        {
            List<Question> qs = new();
            foreach (var question in questions)
            {

                List<Option> ops = new();
                foreach (var option in question.Options)
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
            return qs;


        }


        
        public int DeleteQuestion(int id)
        {
            Question? q = context.Questions.Where(q => q.Id == id).FirstOrDefault();
            if (q == null)
            {
                throw new Exception("Question not found");
            }

            context.Remove(q);
            return context.SaveChanges();
        }
    }
}
