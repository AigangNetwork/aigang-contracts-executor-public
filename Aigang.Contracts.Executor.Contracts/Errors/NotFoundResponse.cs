using Aigang.Contracts.Executor.Domain.Errors;

namespace Aigang.Platform.Contracts.Errors
{
    public class NotFoundResponse : ErrorResponse
    {
        public NotFoundResponse()
        {
            this.Reason = ErrorReasons.NotFound;
        }
        public NotFoundResponse(string message)
        {
            this.Reason = ErrorReasons.NotFound;
            this.Message = message;
        }
    }
}
