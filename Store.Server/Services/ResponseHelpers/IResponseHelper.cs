
namespace Store.Server.Services.ResponseHelpers
{
    public interface IResponseHelper
    {
        public ObjectResult GetStatusResponse(GeneralResponse<object> response);
        public ObjectResult GetStatusResponseWData<T>(GeneralResponse<T> response);
        public GeneralResponse<object> UnauthorizedResponse(string message);
        public GeneralResponse<object> ErrorResponse(string message, HttpStatusCode statusCode);
        public GeneralResponse<object> SuccessResponse();
        public GeneralResponse<T> ErrorResponseWData<T>(string message, HttpStatusCode statusCode);
        public GeneralResponse<T> SuccessResponseWData<T>(T data);
        public GeneralResponse<T> NoContentResponseWData<T>();
    }
}
