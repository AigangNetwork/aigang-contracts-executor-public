using System.Threading.Tasks;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Contracts.Executor.Handlers.Utils;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
    public class IsClaimableHandler : HandlerBase<IsClaimableRequest, IsClaimableResponse>
    {
        public IsClaimableHandler(ILog logger) : base(logger) { }

        protected override Task<ValidationFailure> ValidateRequestAsync(IsClaimableRequest request)
        {
            return Task.FromResult(new ValidationFailure());
        }
        
        protected override async Task<IsClaimableResponse> HandleCoreAsync(IsClaimableRequest request)
        {
            var premiumCalculator = await ContractsFactory.CreatePremiumCalculatorFromAddressAsync(request.ProductAddress);
            
            return new IsClaimableResponse
            {
                IsClaimable = await premiumCalculator.IsClaimableAsync(request.MobileData)
            };
        }
    }
}