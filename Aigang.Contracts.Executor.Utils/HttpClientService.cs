using System.Net;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Domain.Errors;

namespace Aigang.Contracts.Executor.Utils
{
    public class HttpClientService
    {
        public static void ThrowHandledException(HttpStatusCode statusCode, string errorMessage)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ValidationFailedException(new ErrorResponse(ErrorReasons.NotFound, errorMessage));
                case HttpStatusCode.BadRequest:
                    throw new ValidationFailedException(new ErrorResponse(ValidationErrorReasons.BadRequest, errorMessage));
                default: //HttpStatusCode.InternalServerError
                    throw new ExternalServiceFailedException(new ErrorResponse(ErrorReasons.ExternalServerError, errorMessage));
            }
        }

        public static void ThrowHandledException(HttpStatusCode statusCode, string errorMessage, string reason)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ValidationFailedException(new ErrorResponse(reason, errorMessage));
                case HttpStatusCode.BadRequest:
                    throw new ValidationFailedException(new ErrorResponse(reason, errorMessage));
                default:
                    throw new ExternalServiceFailedException(new ErrorResponse(reason, errorMessage));
            }
        }
    }
}