namespace Store.Server.Services.ResponseHelpers
{
    public class ResponseHelper : IResponseHelper
    {
        public ObjectResult GetStatusResponse(GeneralResponse<object> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.InternalServerError => new ObjectResult(response) { StatusCode = 500 },
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = 204 },
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.Conflict => new ConflictObjectResult(response),
                HttpStatusCode.Forbidden => new ObjectResult(response) { StatusCode = 403 },
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };
        }

        public ObjectResult GetStatusResponseWData<T>(GeneralResponse<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.InternalServerError => new ObjectResult(response) { StatusCode = 500 },
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = 204 },
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.Conflict => new ConflictObjectResult(response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };
        }

        public GeneralResponse<object> UnauthorizedResponse(string message) => new() { IsSuccess = false, ErrorMessage = message, StatusCode = HttpStatusCode.Unauthorized };

        public GeneralResponse<object> ErrorResponse(string message, HttpStatusCode statusCode) => new() { IsSuccess = false, ErrorMessage = message, StatusCode = statusCode };

        public GeneralResponse<object> SuccessResponse() => new() { IsSuccess = true, StatusCode = HttpStatusCode.OK };

        public GeneralResponse<T> ErrorResponseWData<T>(string message, HttpStatusCode statusCode) => new() { IsSuccess = false, ErrorMessage = message, StatusCode = statusCode };

        public GeneralResponse<T> SuccessResponseWData<T>(T data) => new() { IsSuccess = true, StatusCode = HttpStatusCode.OK, Data = data };

        public GeneralResponse<T> NoContentResponseWData<T>() => new() { IsSuccess = true, StatusCode = HttpStatusCode.NoContent };
    }
}
