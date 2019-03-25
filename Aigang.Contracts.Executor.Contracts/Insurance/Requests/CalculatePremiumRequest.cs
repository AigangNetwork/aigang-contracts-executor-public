using Aigang.Contracts.Executor.Domain.DeviceData;

namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class CalculatePremiumRequest : BaseRequest
    {
        public int ProductTypeId { get; set; }
        public string ProductAddress { get; set; }
        public MobileData MobileData { get; set; }
    }
}