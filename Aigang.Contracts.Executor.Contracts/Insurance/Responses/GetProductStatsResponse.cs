namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class GetProductStatsResponse : BaseResponse
    {
        public bool Paused { get; set; }
        
        public decimal ProductTokensPool { get; set; }
        
        public int PoliciesLength { get; set; }
        
        public decimal TotalCalculatedPayouts { get; set; }
        
        public int PayoutsCount { get; set; }
        
        public decimal TotalPayouts { get; set; }
    }
}