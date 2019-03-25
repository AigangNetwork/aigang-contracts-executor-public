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
    public class AddPolicyHandler : HandlerBase<AddPolicyRequest, AddPolicyResponse>
	{
        public AddPolicyHandler(ILog logger) : base(logger)
        {
        }

		protected override async Task<ValidationFailure> ValidateRequestAsync(AddPolicyRequest request)
		{
			var walletAddress = ConfigurationManager.GetString("publicKeyInsurance");
			var balance = await BlockchainExplorerService.GetEthBalance(walletAddress);
			var minimumBalance = ConfigurationManager.GetDecimal("MinimumWalletBalance");

			if (balance <= minimumBalance)
			{
				var message =
					$"Adding policy not possible. Wallet balance is below minimum. Minimum wallet balance: {minimumBalance} ETH. Current wallet balance {balance} ETH. Wallet addresss: {walletAddress}";
				
				Logger.Error(message);
				EmailSender.SendOutOfBalanceEmail("Add Policy FAILED", message);

				return new ValidationFailure
				{
					Reason = ErrorReasons.WalletBalanceBelowMinimum,
					Message = "Wallet balance is below minimum balance."
				};
			}

			return new ValidationFailure();
		}

        protected override async Task<AddPolicyResponse> HandleCoreAsync(AddPolicyRequest request)
        {
	        var insuranceProductContract = await ContractsFactory.CreateProductFromAddressAsync(request.ProductAddress);

	        AddPolicyResponse response = new AddPolicyResponse();

	        response.TxId = await insuranceProductContract.AddPolicyAsync(request.PolicyId, request.Payout, request.Properties);

	        return response;
        }
    }
}