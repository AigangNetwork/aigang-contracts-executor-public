﻿using System;
using Aigang.Contracts.Executor.Domain;

namespace Aigang.Contracts.Executor.Contracts.Insurance
{
    public class GetProductResponse : BaseResponse
    {
        public string Address { get; set; }
        
        public string PremiumCalcultator { get; set; }
        
        public string InvestorsPool { get; set; }
        
        public DateTime StartDateUtc { get; set; }
        
        public DateTime EndDateUtc { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int PolicyTermInSeconds { get; set; }
        
        public decimal BasePremium { get; set; }
        
        public decimal Payout { get; set; }
        
        public int Loading { get; set; }
        
        public int PoliciesLimit { get; set; }
        
        public decimal ProductPoolLimit { get; set; }
        
        public DateTime CreatedUtc { get; set; }
        
    }
}