using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Aigang.Contracts.Executor.Blockchain.Client.Contracts.DTO
{
    // Properties Order should be strict as in ABI
    [FunctionOutput]
    public class ProductStatsDto
    {
        [Parameter("uint", 1)]
        public int Paused {get; set;}
        
        [Parameter("uint256", 2)]
        public BigInteger TokenBalance {get; set;}
        
        [Parameter("uint256", 3)]
        public BigInteger PoliciesLength {get; set;}
        
        [Parameter("uint256", 4)]
        public BigInteger PoliciesTotalCalculatedPayouts {get; set;}

        [Parameter("uint256", 5)]
        public BigInteger PoliciesPayoutsCount {get; set;}
        
        [Parameter("uint256", 6)]
        public BigInteger PoliciesTotalPayouts {get; set;}
    }
}