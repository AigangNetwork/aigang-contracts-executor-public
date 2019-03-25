using System;

namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class AddPolicyRequest : BaseInsuranceRequest
    {
        public decimal Payout { get; set; }
        public string Properties { get; set; }
    }
}