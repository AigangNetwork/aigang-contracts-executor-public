using Aigang.Contracts.Executor.Domain.Errors;

namespace Aigang.Platform.Domain.Base
{
    public static class ErrorReasonsChecker
    {
        public static bool IsValidationError(string errorReasons)
        {
            return errorReasons != null
                   && !IsNotFound(errorReasons)
                   && !IsUnauthorized(errorReasons)
                   && !IsGeneralError(errorReasons);
        }

        public static bool IsGeneralError(string errorReasons)
        {
            return errorReasons == ErrorReasons.InternalServerError
                || errorReasons == ErrorReasons.ContractBalanceBelowMinimum
                || errorReasons == ErrorReasons.WalletBalanceBelowMinimum;
        }

        public static bool IsNotFound(string errorReasons)
        {
            return errorReasons == ErrorReasons.NotFound;
        }

        public static bool IsUnauthorized(string errorReasons)
        {
            return errorReasons == ErrorReasons.Unauthorized;
        }
        
        public static bool IsContractBalanceBelowMinimum(string errorReasons)
        {
            return errorReasons == ErrorReasons.ContractBalanceBelowMinimum;
        }
        
        public static bool IsWalletBalanceBellowMinimum(string errorReasons)
        {
            return errorReasons == ErrorReasons.WalletBalanceBelowMinimum;
        }
    }
}
