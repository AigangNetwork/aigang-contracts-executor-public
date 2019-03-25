using System.Threading.Tasks;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Contracts.Executor.Handlers.Utils;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
    public class ValidateDataHandler : HandlerBase<ValidateDataRequest, ValidateDataResponse>
    {
        public ValidateDataHandler(ILog logger) : base(logger) { }

        protected override Task<ValidationFailure> ValidateRequestAsync(ValidateDataRequest request)
        {
            return Task.FromResult(new ValidationFailure());
        }
        
        protected override async Task<ValidateDataResponse> HandleCoreAsync(ValidateDataRequest request)
        {
            var premiumCalculator = await ContractsFactory.CreatePremiumCalculatorFromAddressAsync(request.ProductAddress);
            
            return new ValidateDataResponse
            {
                ValidationResultCode = await premiumCalculator.ValidateAsync(request.MobileData)
            };
        }
    }
}