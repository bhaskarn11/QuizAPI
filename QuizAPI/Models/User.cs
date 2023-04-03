namespace QuizAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public bool IsDisabled { get; set; } = false;

        public int AssesmentId { get; set; }
        public ICollection<Assessment> Assesments { get; set; } = new List<Assessment>();
        public UserType UserType { get; set; }

    }

    public enum UserType
    {
        TEACHER,
        STUDENT
    }
}
