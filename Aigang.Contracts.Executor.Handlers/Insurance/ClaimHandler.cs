using System.Threading.Tasks;
using Aigang.Contracts.Executor.Blockchain.Client.Utils;
using Aigang.Contracts.Executor.Contracts.Errors;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Domain.Errors;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Contracts.Executor.Handlers.Utils;
using Aigang.Contracts.Executor.Utils;
using Aigang.Transactions.Utils;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
	public class ClaimHandler : HandlerBase<AddClaimRequest, ClaimResponse>
	{
        public ClaimHandler(ILog logger) : base(logger)
        {
	       
        }
		
		protected override async Task<ValidationFailure> ValidateRequestAsync(AddClaimRequest request)
		{
			var balance = await BlockchainExplorerService.GetTokenBalance(request.ProductAddress);

			var minimumBalance = ConfigurationManager.GetDecimal("MinimumContractBalance");

			if (balance <= minimumBalance)
			{
				var message =
					$"Claiming is not possible. Contract balance is below minimum. Minimum contract balance: {minimumBalance} AIX. Current contract balance {balance} AIX. Product. Product address: {request.ProductAddress}";
				
				Logger.Error(message);
				EmailSender.SendOutOfBalanceEmail("Claim FAILED", message);

				return new ValidationFailure
				{
					Reason = ErrorReasons.ContractBalanceBelowMinimum,
					Message = "Contract balance is below minimum balance."
				};
			}

			return new ValidationFailure();
		}

        protected override async Task<ClaimResponse> HandleCoreAsync(AddClaimRequest request)
        {
	        var productContract = await ContractsFactory.CreateProductFromAddressAsync(request.ProductAddress);
	        
	        ClaimResponse response = new ClaimResponse();

	        response.TxId = await productContract.ClaimAsync(request.PolicyId, request.ClaimProperties);
	        
	        return response;
        }
    }
}