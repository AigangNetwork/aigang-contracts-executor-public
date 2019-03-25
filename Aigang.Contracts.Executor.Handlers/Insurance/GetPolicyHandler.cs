using System.Threading.Tasks;
using Aigang.Contracts.Executor.Blockchain.Client.Contracts;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Handlers.Base;
using Aigang.Ethernscan.Client;
using log4net;

namespace Aigang.Contracts.Executor.Handlers
{
    public class GetPolicyHandler : HandlerBase<BaseInsuranceRequest, GetPolicyResponse>
    {

        public GetPolicyHandler(ILog logger) : base(logger)
        {
            
        }
        
        protected override Task<ValidationFailure> ValidateRequestAsync(BaseInsuranceRequest request)
        {
            return Task.FromResult(new ValidationFailure());
        }

        protected override async Task<GetPolicyResponse> HandleCoreAsync(BaseInsuranceRequest request)
        {
            var response = new GetPolicyResponse();
            
            var abi = await CachedABIManager.GetAbiAsync(request.ProductAddress);
            var insuranceProductContract = new InsuranceProduct(request.ProductAddress, abi);
	        
            response.Policy = await insuranceProductContract.GetPolicyAsync(request.PolicyId);
	        
            return response;
        }
    }
}