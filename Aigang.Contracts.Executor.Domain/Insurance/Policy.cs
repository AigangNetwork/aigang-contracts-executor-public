using System;

namespace Aigang.Contracts.Executor.Domain
{
    public class Policy
    {
        public string Id { get; set; }
        
        public DateTime StartUtc { get; set; }
        
        public DateTime EndUtc { get; set; }
        
        public DateTime PayoutUtc { get; set; }
        
        public string Properties { get; set; }
        
        public string ClaimProperties { get; set; }

        public bool IsCanceled { get; set; }
        
        public decimal CalculatedPayout { get; set; }
        
        public decimal Payout { get; set; }

        public decimal Premium { get; set; }
        
        public PolicyStatus Status { get; set; }
        
        public DateTime ModifiedUtc { get; set; }
    }
}