using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Aigang.Contracts.Executor.Blockchain.Client.Contracts.DTO
{
    // Properties Order should be strict as in ABI
    [FunctionOutput]
    public class ProductDto
    {
        [Parameter("address", "premiumCalculator", 1)]
        public string PremiumCalculator {get; set;}
        
        [Parameter("address", "investorsPool", 2)]
        public string InvestorsPool {get; set;}
        
        [Parameter("uint256", "utcProductStartDate", 3)]
        public BigInteger ProductStartDateUtc {get; set;}
        
        [Parameter("uint256", "utcProductEndDate", 4)]
        public BigInteger ProductEndDateUtc {get; set;}
        
        [Parameter("string", "title", 5)]
        public string Title {get; set;}
        
        [Parameter("string", "description", 6)]
        public string Description {get; set;}
        
        [Parameter("uint256", "policyTermInSeconds", 7)]
        public BigInteger PolicyTermInSeconds {get; set;}
        
        [Parameter("uint256", "basePremium", 8)]
        public BigInteger BasePremium {get; set;}
        
        [Parameter("uint256", "payout", 9)]
        public BigInteger Payout {get; set;}
        
        [Parameter("uint256", "loading", 10)]
        public BigInteger Loading {get; set;}
        
        [Parameter("uint256", "policiesLimit", 11)]
        public BigInteger PoliciesLimit {get; set;}
        
        [Parameter("uint256", "productPoolLimit", 12)]
        public BigInteger ProductPoolLimit {get; set;}
        
        [Parameter("uint256", "created", 13)]
        public BigInteger CreatedUtc {get; set;}
    }
}