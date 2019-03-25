using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Aigang.Contracts.Executor.Blockchain.Client.Contracts.DTO
{
    // Properties Order should be strict as in ABI
    [FunctionOutput]
    public class PolicyDto
    {
        [Parameter("address", "owner", 1)]
        public string Owner {get; set;}
        
        [Parameter("uint32", "utcStart", 2)]
        public BigInteger StartUtc {get; set;}
        
        [Parameter("uint32", "utcEnd", 3)]
        public BigInteger EndUtc {get; set;}
        
        [Parameter("uint32", "utcPayoutDate", 4)]
        public BigInteger PayoutUtc {get; set;}
        
        [Parameter("uint256", "premium", 5)]
        public BigInteger Premium {get; set;}
        
        [Parameter("uint256", "calculatedPayout", 6)]
        public BigInteger CalculatedPayout {get; set;}

        [Parameter("string", "properties", 7)]
        public string Properties {get; set;}
        
        [Parameter("uint256", "payout", 8)]
        public BigInteger Payout {get; set;}
        
        [Parameter("string", "claimProperties", 9)]
        public string ClaimProperties {get; set;}
        
        [Parameter("uint", "isCanceled", 10)]
        public int IsCanceled {get; set;}
        
        [Parameter("uint", "created", 11)]
        public int Created {get; set;}
    }
}