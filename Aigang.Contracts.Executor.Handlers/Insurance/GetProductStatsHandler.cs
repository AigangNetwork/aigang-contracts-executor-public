using System.Threading.Tasks;
using Aigang.Contracts.Executor.Blockchain.Client.Contracts;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Ethernscan.Client;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
    public class GetProductStatsHandler : HandlerBase<GetProductStatsRequest, GetProductStatsResponse>
    {
        public GetProductStatsHandler(ILog logger) : base(logger)
        {
            
        }
        
        protected override Task<ValidationFailure> ValidateRequestAsync(GetProductStatsRequest request)
        {
            return Task.FromResult(new ValidationFailure());
        }

        protected override async Task<GetProductStatsResponse> HandleCoreAsync(GetProductStatsRequest request)
        {
            var abi = await CachedABIManager.GetAbiAsync(request.ProductAddress);
            var insuranceProductContract = new InsuranceProduct(request.ProductAddress, abi);
          
        
            var stats = await insuranceProductContract.GetProductStatsAsync();
            
            return new GetProductStatsResponse
            {
                Paused = stats.Paused,
                PoliciesLength = stats.PoliciesLength,
                ProductTokensPool = stats.TokenBalance,
                PayoutsCount = stats.PayoutsCount,
                TotalCalculatedPayouts = stats.TotalCalculatedPayouts,
                TotalPayouts = stats.TotalPayouts
            };
        }
    }
}