namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class GetProductStatsRequest : BaseRequest
    {
        public int ProductTypeId { get; set; }
        public string ProductAddress { get; set; }
    }
}