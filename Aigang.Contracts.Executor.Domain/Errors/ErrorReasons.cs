namespace Aigang.Contracts.Executor.Domain.Errors
{
    public class ErrorReasons
    {
        // 5**
        public const string InternalServerError = nameof(InternalServerError); // 500
        public const string ContractBalanceBelowMinimum = nameof(ContractBalanceBelowMinimum);
        public const string WalletBalanceBelowMinimum = nameof(WalletBalanceBelowMinimum);
        
        // External service errors
        public const string ExternalServerError = nameof(ExternalServerError); // 503
        
        // 4**
        public const string EmptyRequest = nameof(EmptyRequest);
        public const string Unauthorized = nameof(Unauthorized); // 401
        public const string NotFound = nameof(NotFound); // 404
        public const string ValidationFailed = nameof(ValidationFailed); // 400
    }
}