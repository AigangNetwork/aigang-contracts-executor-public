using System;
using System.Net;
using Aigang.Contracts.Executor.Contracts.Errors;
using Aigang.Contracts.Executor.Domain.Errors;
using Aigang.Platform.Domain.Base;

namespace Aigang.Contracts.Executor.API.Utils
{
    public static class HttpStatusResolver
    {
        public static HttpStatusCode ResolveStatusCodeFromErrors(ErrorResponse errorResponse)
        {
            if (errorResponse?.Reason != null)
            {
                if (ErrorReasonsChecker.IsValidationError(errorResponse.Reason))
                {
                    return HttpStatusCode.BadRequest;
                }

                if (ErrorReasonsChecker.IsGeneralError(errorResponse.Reason))
                {
                    return HttpStatusCode.InternalServerError;
                }

                if (ErrorReasonsChecker.IsNotFound(errorResponse.Reason))
                {
                    return HttpStatusCode.NotFound;
                }

                if (ErrorReasonsChecker.IsUnauthorized(errorResponse.Reason))
                {
                    return HttpStatusCode.Unauthorized;
                }

                throw new Exception("Error collection contains mixed types of errors");
            }

            return HttpStatusCode.OK;
        }
    }
}
