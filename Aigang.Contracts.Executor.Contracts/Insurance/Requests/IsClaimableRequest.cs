using Aigang.Contracts.Executor.Domain.DeviceData;

namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class IsClaimableRequest : GetContractInfoRequest
    {
        public MobileData MobileData { get; set; }
    }
}