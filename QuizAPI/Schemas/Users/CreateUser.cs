namespace QuizAPI.Schemas.Users
{
    public class CreateUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsDisabled { get; set; } = false;
        public UserType UserType { get; set; }
    }
}
