namespace QuizAPI.Schemas
{
    public class Response<T>
    {
        public T Data { get; set; } = default!;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Response(bool success)
        {
            Success = success;
        }

        public Response(T data, bool success)
        {
            Data = data;
            Success = success;
        }

        public Response(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
