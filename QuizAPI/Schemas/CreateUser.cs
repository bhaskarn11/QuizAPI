namespace QuizAPI.Schemas
{
    public class CreateUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
