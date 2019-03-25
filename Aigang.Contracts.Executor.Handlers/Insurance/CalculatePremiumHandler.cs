using System.Threading.Tasks;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Contracts.Executor.Handlers.Utils;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
    public class CalculatePremiumHandler : HandlerBase<CalculatePremiumRequest, CalculatePremiumResponse>
    {
        public CalculatePremiumHandler(ILog logger) : base(logger) { }

        protected override Task<ValidationFailure> ValidateRequestAsync(CalculatePremiumRequest request)
        {
            return Task.FromResult(new ValidationFailure());
        }
        
        protected override async Task<CalculatePremiumResponse> HandleCoreAsync(CalculatePremiumRequest request)
        {
            var premiumCalculator = await ContractsFactory.CreatePremiumCalculatorFromAddressAsync(request.ProductAddress);
            
            return new CalculatePremiumResponse
            {
                Premium = await premiumCalculator.CalculatePremiumAsync(request.MobileData)
            };
        }
    }
}