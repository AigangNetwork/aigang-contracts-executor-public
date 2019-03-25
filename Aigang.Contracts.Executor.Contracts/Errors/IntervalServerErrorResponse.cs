using Aigang.Contracts.Executor.Domain.Errors;

namespace Aigang.Contracts.Executor.Contracts.Errors
{
    public class InternalServerErrorResponse : ErrorResponse
    {
        public InternalServerErrorResponse()
        {
            this.Reason = ErrorReasons.InternalServerError;
        }

        public InternalServerErrorResponse(string message)
        {
            this.Reason = ErrorReasons.InternalServerError;
            this.Message = message;
        }
    }
}
