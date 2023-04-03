namespace QuizAPI.Schemas
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";

        public Response(T data, bool success)
        {
            Data = data;
            Success = success;
        }
    }
}
