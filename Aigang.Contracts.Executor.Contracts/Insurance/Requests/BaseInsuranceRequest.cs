namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class BaseInsuranceRequest : BaseRequest
    {
        public string ProductTypeId { get; set; }
        public string ProductAddress { get; set; }
        public string PolicyId { get; set; }
    }
}