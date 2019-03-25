using System;
using System.Threading.Tasks;
using Aigang.Contracts.Executor.Blockchain.Client.Contracts.DTO;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Utils;
using log4net;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace Aigang.Contracts.Executor.Blockchain.Client.Contracts
{
    public class InsuranceProduct : IInsuranceProduct
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(InsuranceProduct));
        
        private readonly Contract _contract;
        private readonly string _publicKey;    
        private readonly HexBigInteger _gasPrice;
        
        public InsuranceProduct(string address, string abi)
        {
            var web3 = GetWeb3();
            _publicKey = ConfigurationManager.GetString("publicKeyInsurance");
            _gasPrice = new HexBigInteger(ConfigurationManager.GetBigInt("Ethereum:gasPrice"));
            _contract = web3.Eth.GetContract(abi, address);
        }
        
        public async Task<Product> GetProductAsync()
        {
            var functionName = "getProductDetails";
            var func = _contract.GetFunction(functionName);
            
            
            var productDto = new ProductDto();
            
            try
            {
                productDto = await func.CallDeserializingToObjectAsync<ProductDto>();
            }
            catch (ArgumentNullException e) // catching this error because after successful transaction is time frame while blockchain data is not updated and returns unknown result. We are getting: Value cannot be null. Parameter name: value
            {
                _logger.Warn($"Cant deserialize contract response. functionName: {functionName}", e);
            }

            
            var result = new Product();

            result.InvestorsPool = productDto.InvestorsPool;
            result.PremiumCalcultator = productDto.PremiumCalculator;
            result.StartDateUtc = DateTimeHelper.UnixTimeStampToDateTime(productDto.ProductStartDateUtc);
            result.EndDateUtc = DateTimeHelper.UnixTimeStampToDateTime(productDto.ProductEndDateUtc);
            result.Title = productDto.Title;
            result.Description = productDto.Description;
            result.PolicyTermInSeconds = (int)productDto.PolicyTermInSeconds;
            result.BasePremium = Web3.Convert.FromWei(productDto.BasePremium);
                
            result.Payout = Web3.Convert.FromWei(productDto.Payout);
            result.Loading = (int)productDto.Loading;
            result.PoliciesLimit = (int)productDto.PoliciesLimit;
            result.ProductPoolLimit = Web3.Convert.FromWei(productDto.ProductPoolLimit);
            
            result.CreatedUtc = DateTimeHelper.UnixTimeStampToDateTime(productDto.CreatedUtc);
            
            return result;
        }
        
        public async Task<ProductStats> GetProductStatsAsync()
        {
            var functionName = "getProductStats";
            var func = _contract.GetFunction(functionName);
            
            
            var dto = new ProductStatsDto();
            
            try
            {
                dto = await func.CallDeserializingToObjectAsync<ProductStatsDto>();
            }
            catch (ArgumentNullException e) // catching this error because after successful transaction is time frame while blockchain data is not updated and returns unknown result. We are getting: Value cannot be null. Parameter name: value
            {
                _logger.Warn($"Cant deserialize contract response. functionName: {functionName}", e);
            }

            
            var result = new ProductStats();
            
            result.Paused = dto.Paused == 1;
            result.TokenBalance = Web3.Convert.FromWei(dto.TokenBalance);
            result.PoliciesLength = (int)dto.PoliciesLength;

            result.TotalCalculatedPayouts = Web3.Convert.FromWei(dto.PoliciesTotalCalculatedPayouts);
            result.PayoutsCount = (int)dto.PoliciesPayoutsCount;

            result.TotalPayouts =  Web3.Convert.FromWei(dto.PoliciesTotalPayouts);

            return result;
        }
        
        public async Task<Policy> GetPolicyAsync(string policyId)
        {
            var functionName = "policies";
            var func = _contract.GetFunction(functionName);
            
            
            var policyDto = new PolicyDto();
            
            try
            {
                policyDto = await func.CallDeserializingToObjectAsync<PolicyDto>(policyId);
            }
            catch (ArgumentNullException e) // catching this error because after successful transaction is time frame while blockchain data is not updated and returns unknown result. We are getting: Value cannot be null. Parameter name: value
            {
                _logger.Warn($"Cant deserialize contract response. functionName: {functionName}, policyId: {policyId}", e);
            }

            var result = new Policy();
            
            result.Id = policyId;
            result.StartUtc = DateTimeHelper.UnixTimeStampToDateTime(policyDto.StartUtc);
            result.EndUtc = DateTimeHelper.UnixTimeStampToDateTime(policyDto.EndUtc);
            result.PayoutUtc = DateTimeHelper.UnixTimeStampToDateTime(policyDto.PayoutUtc);
            result.IsCanceled = policyDto.IsCanceled == 1;
            result.Premium = Web3.Convert.FromWei(policyDto.Premium);
            result.CalculatedPayout = Web3.Convert.FromWei(policyDto.CalculatedPayout);
            result.Properties = policyDto.Properties;
            result.Payout = Web3.Convert.FromWei(policyDto.Payout);
            result.ClaimProperties = policyDto.ClaimProperties;

            return result;
        }
        
        public async Task<string> AddPolicyAsync(string id, 
            decimal payout,
            string properties)
        {
            HexBigInteger gasLimit = new HexBigInteger(ConfigurationManager.GetBigInt("Ethereum:insurance:addPolicy:gasLimit"));

            var addProduct = _contract.GetFunction("addPolicy");

            var payoutInWeis = Web3.Convert.ToWei(payout);
            
            var data = new Object[3];
            data[0] = id;
            data[1] = payoutInWeis;
            data[2] = properties;

            var resultTx = await addProduct.SendTransactionAsync(
                _publicKey, 
                gasLimit, 
                _gasPrice,
                new HexBigInteger(0),
                data);
            
            return resultTx;
        }

        public async Task<string> ClaimAsync(string id, string claimProperties)
        {
            HexBigInteger gasLimit = new HexBigInteger(ConfigurationManager.GetBigInt("Ethereum:insurance:claim:gasLimit"));
            var claim = _contract.GetFunction("claim");

            var data = new Object[2];
            data[0] = id;
            data[1] = claimProperties;
            
            var resultTx = await claim.SendTransactionAsync(
                _publicKey,
                gasLimit,
                _gasPrice,
                new HexBigInteger(0),
                data);
            
            return resultTx;
        }

        private Web3 GetWeb3()
        {
            var privateKey = ConfigurationManager.GetString("privateKeyInsurance");
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var web3 = new Web3(account, ConfigurationManager.GetString("NetworkProvider"));

            return web3;
        }
        
        public async Task<string> GetPremiumCalculatorAddressAsync()
        {
            var getPremiumCalculatorAddress = _contract.GetFunction("premiumCalculator");
            var premiumCalculatorAddress = await getPremiumCalculatorAddress.CallAsync<string>();

            return premiumCalculatorAddress;
        }
    }

    public interface IInsuranceProduct
    {
        Task<Product> GetProductAsync();
        Task<ProductStats> GetProductStatsAsync();
        Task<Policy> GetPolicyAsync(string policyId);
        
        Task<string> AddPolicyAsync(string id, decimal payout, string properties);

        Task<string> ClaimAsync(string id, string claimProperties);
       
        Task<string> GetPremiumCalculatorAddressAsync();
    }
}