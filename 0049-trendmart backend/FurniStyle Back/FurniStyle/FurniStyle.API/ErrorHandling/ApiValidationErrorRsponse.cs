namespace FurniStyle.API.ErrorHandling
{
    public class ApiValidationErrorRsponse:ApiErrorResponse
    {

        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public ApiValidationErrorRsponse() : base(400)
        {
        }
    }
}
