namespace QuizAPI.Schemas
{
    public class TokenResponse
    {
        public string? Token { get; set; } = default;
        public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; }

        public TokenResponse(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public TokenResponse(string token, bool success, string errorMessage)
        {
            Token = token;
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
