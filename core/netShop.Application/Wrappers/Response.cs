namespace netShop.Application.Wrappers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; } = false;
        public string[] Errors { get; set; }
        public string Message { get; set; }

        public Response()
        {
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            Errors = null;
            Data = default(T);
        }

        public Response(string message, string[] errors)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
            Data = default(T);
        }
    }
}