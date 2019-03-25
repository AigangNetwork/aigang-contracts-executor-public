using Aigang.Contracts.Executor.Domain;

namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class GetPolicyResponse : BaseResponse
    {
        public Policy Policy { get; set; }
    }
}