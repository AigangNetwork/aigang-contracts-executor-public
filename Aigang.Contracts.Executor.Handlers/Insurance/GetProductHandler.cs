using System.Threading.Tasks;
using Aigang.Contracts.Executor.Blockchain.Client.Contracts;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Ethernscan.Client;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
    public class GetProductHandler : HandlerBase<GetProductRequest, GetProductResponse>
    {
        public GetProductHandler(ILog logger) : base(logger)
        {
            
        }
        
        protected override Task<ValidationFailure> ValidateRequestAsync(GetProductRequest request)
        {
            return Task.FromResult(new ValidationFailure());
        }

        protected override async Task<GetProductResponse> HandleCoreAsync(GetProductRequest request)
        {
            var abi = await CachedABIManager.GetAbiAsync(request.ProductAddress);
            var insuranceProductContract = new InsuranceProduct(request.ProductAddress, abi);
          
            var product = await insuranceProductContract.GetProductAsync();
            
            return new GetProductResponse
            {
                Address = request.ProductAddress,
                PremiumCalcultator = product.PremiumCalcultator,
                InvestorsPool = product.InvestorsPool,
                StartDateUtc = product.StartDateUtc,
                EndDateUtc = product.EndDateUtc,
                Title = product.Title,
                Description = product.Description,
                PolicyTermInSeconds = product.PolicyTermInSeconds,
                BasePremium = product.BasePremium,
                Payout = product.Payout,
                Loading = product.Loading,
                PoliciesLimit = product.PoliciesLimit,
                ProductPoolLimit = product.ProductPoolLimit,
                CreatedUtc = product.CreatedUtc
            };
        }
    }
}