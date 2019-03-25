using System.Threading.Tasks;
using Aigang.Contracts.Executor.Blockchain.Client.Contracts;
using Aigang.Ethernscan.Client;

namespace Aigang.Contracts.Executor.Handlers.Utils
{
    public static class ContractsFactory
    {
        public static async Task<IInsuranceProduct> CreateProductFromAddressAsync(string productAddress)
        {
            var productAbi = await CachedABIManager.GetAbiAsync(productAddress);
            var insuranceProductContract = new InsuranceProduct(productAddress, productAbi);

            return insuranceProductContract;
        }
        
        public static async Task<PremiumCalculator> CreatePremiumCalculatorFromAddressAsync(string productAddress)
        {
            var productAbi = await CachedABIManager.GetAbiAsync(productAddress);
            var insuranceProductContract = new InsuranceProduct(productAddress, productAbi);
            
            var premiumCalculatorAddress = await insuranceProductContract.GetPremiumCalculatorAddressAsync();
            var premiumCalculatorAbi = await CachedABIManager.GetAbiAsync(premiumCalculatorAddress);
            
            var premiumCalculatorContract = new PremiumCalculator(premiumCalculatorAddress, premiumCalculatorAbi);

            return premiumCalculatorContract;
        }
    }
}