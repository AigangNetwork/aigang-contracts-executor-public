using System;
using Aigang.Contracts.Executor.Domain.Errors;

namespace Aigang.Contracts.Executor.Domain
{
    public class ExternalServiceFailedException : Exception
    {     
        public ErrorResponse Error { get; set; }

        public ExternalServiceFailedException(ErrorResponse error)
        {
            Error = error;
        }
    }
}