﻿namespace QuizAPI.Schemas.Quiz
{
    public class AddOption
    {
        public string? Content { get; set; }
        public bool IsRightOption { get; set; } = false;
    }
}
