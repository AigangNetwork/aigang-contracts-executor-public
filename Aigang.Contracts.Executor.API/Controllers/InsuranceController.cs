using System.Threading.Tasks;
using Aigang.Contracts.Executor.Contracts.Errors;
using Aigang.Contracts.Executor.Contracts.Insurance;
using Aigang.Contracts.Executor.Domain.Errors;
using Aigang.Contracts.Executor.Handlers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Aigang.Contracts.Executor.API.Controllers
{
    [Route("api/insurance")]
    public class InsuranceController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(InsuranceController));
        
        private readonly GetProductHandler _getProductHandler;
        
        private readonly ClaimHandler _claimHandler;
        private readonly AddPolicyHandler _addPolicyHandler;
        private readonly GetPolicyHandler _getPolicyHandler;
        private readonly CalculatePremiumHandler _calculatePremiumHandler;
        private readonly ValidateDataHandler _validateDataHandler;
        private readonly IsClaimableHandler _isClaimableHandler;
        private readonly GetProductStatsHandler _getProductStatsHandler;

        
        public InsuranceController()
        {
            _claimHandler = new ClaimHandler(_logger);
            _addPolicyHandler = new AddPolicyHandler(_logger);
            _getPolicyHandler = new GetPolicyHandler(_logger);
            _calculatePremiumHandler = new CalculatePremiumHandler(_logger);
            _validateDataHandler = new ValidateDataHandler(_logger);
            _isClaimableHandler = new IsClaimableHandler(_logger);
            _getProductHandler = new GetProductHandler(_logger);
            _getProductStatsHandler = new GetProductStatsHandler(_logger);
        }
        
        // GET api/insurance/product/{productTypeId}/{productAddress}/stats
        /// <summary> Get product details.</summary>
        /// <returns> Product json </returns>
        [HttpGet("product/{productTypeId}/{productAddress}/stats")]
        [ProducesResponseType(typeof(GetProductResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> GetProductStats(GetProductStatsRequest request)
        {
            var response = await _getProductStatsHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
        
        // GET api/insurance/product/{productTypeId}/{productAddress}
        /// <summary> Get product details.</summary>
        /// <returns> Product json </returns>
        [HttpGet("product/{productTypeId}/{productAddress}")]
        [ProducesResponseType(typeof(GetProductResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> GetProduct(GetProductRequest request)
        {
            var response = await _getProductHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
        
        // GET api/insurance/policy/{productTypeId}/{productAddress}/{policyId}
        /// <summary> Get policy.  From Transaction.Listener  </summary>
        /// <returns> Policy json </returns>
        [HttpGet("policy/{productTypeId}/{productAddress}/{policyId}")]
        [ProducesResponseType(typeof(GetPolicyResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> GetPolicy(BaseInsuranceRequest request)
        {
            var response = await _getPolicyHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
        
        // POST api/insurance/addpolicy
        /// <summary> Create new policy. From Transaction.Listener </summary>
        /// <returns> TxId </returns>
        [HttpPost("addpolicy")]
        [ProducesResponseType(typeof(AddPolicyResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> AddPolicy([FromBody]AddPolicyRequest request)
        {
            var response = await _addPolicyHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }

        // POST api/insurance/claim
        /// <summary> Registers a claim for policy. From API</summary>
        /// <returns> TxId </returns>
        [HttpPost("claim")]
        [ProducesResponseType(typeof(ClaimResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> Claim([FromBody]AddClaimRequest request)
        {
            var response = await _claimHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
        
        // GET api/insurance/calculatepremium
        /// <summary> Calculate premium for device. From API </summary>
        /// <returns> Premium price </returns>
        [HttpGet("calculatepremium")]
        [ProducesResponseType(typeof(CalculatePremiumResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> CalculatePremium([FromBody]CalculatePremiumRequest request)
        {
            var response = await _calculatePremiumHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
        
        // GET api/insurance/validatedata
        /// <summary> Validate device data for insurace. From API</summary>
        /// <returns> Validation Code </returns>
        [HttpGet("validatedata")]
        [ProducesResponseType(typeof(ValidateDataResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> ValidateData([FromBody]ValidateDataRequest request)
        {
            var response = await _validateDataHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
        

        // GET api/insurance/isclaimable
        /// <summary> Get if policy is claimable by device data</summary>
        /// <returns> IsClaimable </returns>
        [HttpGet("isclaimable")]
        [ProducesResponseType(typeof(IsClaimableResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), 500)]
        public async Task<IActionResult> IsClaimable([FromBody]IsClaimableRequest request)
        {
            var response = await _isClaimableHandler.HandleAsync(request);
            var result = MakeActionResult(response);
            return result;
        }
      
    }
}