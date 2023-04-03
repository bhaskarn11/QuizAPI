using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Data
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {   
        }
        public DbSet<Assessment> Assessments { get; set; }
        // public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AssessmentAnswer> AssessmentAnswers { get; set; }
    }
}
