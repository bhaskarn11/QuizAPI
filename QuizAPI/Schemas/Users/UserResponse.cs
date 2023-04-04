namespace QuizAPI.Schemas.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public bool IsDisabled { get; set; } = false;
        public ICollection<Assessment> Assesments { get; set; } = new List<Assessment>();
        public UserType UserType { get; set; }
    }
}
