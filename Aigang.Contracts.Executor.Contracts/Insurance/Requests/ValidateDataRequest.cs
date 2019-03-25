using Aigang.Contracts.Executor.Domain.DeviceData;

namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class ValidateDataRequest : BaseRequest
    {
        public int ProductTypeId { get; set; }
        public string ProductAddress { get; set; }
        public MobileData MobileData { get; set; }
    }
}