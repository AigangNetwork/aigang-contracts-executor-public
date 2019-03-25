namespace Aigang.Contracts.Executor.Domain
{
    public enum PolicyStatus
    {
        NotSet = 0,
        
        Draft = 1,
        
        Paid = 2,
        
        Claimable = 3,
        
        PaidOut = 4,
        
        Canceled = 5,
        
        PendingPayment = 6,
        
        PendingPayout = 7,
        
        Finished  = 8
    }
}