using System;

namespace Aigang.Contracts.Executor.Domain
{
    public class ProductStats
    {
        public bool Paused { get; set; }
        
        public decimal TokenBalance { get; set; }
        
        public int PoliciesLength { get; set; }

        public decimal TotalCalculatedPayouts { get; set; }

        public int PayoutsCount { get; set; }
        
        public decimal TotalPayouts { get; set; }
    }
}