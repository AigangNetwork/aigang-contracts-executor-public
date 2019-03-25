namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class AddClaimRequest : BaseInsuranceRequest
    {
        public string ClaimProperties { get; set; }
    }
}