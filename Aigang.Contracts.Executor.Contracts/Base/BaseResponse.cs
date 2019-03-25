using System.Net;
using Aigang.Contracts.Executor.Contracts.Errors;
using Aigang.Contracts.Executor.Domain.Errors;

namespace Aigang.Contracts.Executor.Contracts
{
    public class BaseResponse
    {
        public ErrorResponse Error { get; set; }

        public HttpStatusCode? SuccessStatusCode { get; set; }
    }
}