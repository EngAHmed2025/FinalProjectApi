namespace FinalProjectApi.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }


        public ApiExceptionResponse(int stateCode , string message = null , string details = null) :base(stateCode , message)
        {
            Details = details;  
        }
    }
}
