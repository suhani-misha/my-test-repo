namespace SocialSecurity.Shared.Dtos.Common
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public int Code { get; set; }

        // Default constructor
        public Response()
        {
        }

        // Constructor with status and message
        public Response(string status, string message, int code)
        {
            Status = status;
            Message = message;
            Code = code;
        }

        // Constructor with status, message, and data
        public Response(string status, string message, object data, int code)
        {
            Status = status;
            Message = message;
            Data = data;
            Code = code;
        }
    }

}
