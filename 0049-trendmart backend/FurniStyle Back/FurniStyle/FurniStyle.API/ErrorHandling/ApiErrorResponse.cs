namespace FurniStyle.API.ErrorHandling
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => "Bad Request",
                401 => "Not Authorized",
                404 => "OPS....Resource is not Found",
                500 => "Server Error",
                _ => null

            };
            return message;

        }
    }
}
