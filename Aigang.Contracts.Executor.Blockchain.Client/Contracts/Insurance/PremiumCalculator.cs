using System;
using System.Numerics;
using System.Threading.Tasks;
using Aigang.Contracts.Executor.Domain.DeviceData;
using Aigang.Contracts.Executor.Utils;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace Aigang.Contracts.Executor.Blockchain.Client.Contracts
{
    public class PremiumCalculator
    {
        private readonly Contract _contract;

        public PremiumCalculator(string address, string abi)
        {            
            var web3 = new Web3(ConfigurationManager.GetString("NetworkProvider"));
            _contract = web3.Eth.GetContract(abi, address);
        }

        public async Task<decimal> CalculatePremiumAsync(MobileData mobileData)
        {
            var calculatePremium = _contract.GetFunction("calculatePremium");
            var data = new Object[6];
            data[0] = mobileData.BatteryDesignCapacity;
            data[1] = mobileData.ChargeLevel;
            data[2] = mobileData.AgeInMonths;
            data[3] = mobileData.Region;
            data[4] = mobileData.Brand;
            data[5] = mobileData.WearLevel;

            var result = await calculatePremium.CallAsync<BigInteger>(data);

            return Web3.Convert.FromWei(result);
        }

        public async Task<string> ValidateAsync(MobileData mobileData)
        {
            var validate = _contract.GetFunction("validate");
            
            var data = new Object[6];
            data[0] = mobileData.BatteryDesignCapacity;
            data[1] = mobileData.ChargeLevel;
            data[2] = mobileData.AgeInMonths;
            data[3] = mobileData.Region;
            data[4] = mobileData.Brand;
            data[5] = mobileData.WearLevel;

            var result = await validate.CallAsync<byte[]>(data);

            // 'result' is byte[] filled with '\0' characters, we need to clean it
            return System.Text.Encoding.Default.GetString(result).Replace("\0", string.Empty);
        }


        public async Task<bool> IsClaimableAsync(MobileData mobileData)
        {
            var isClaimableFunction = _contract.GetFunction("isClaimable");
            var isClaimable = await isClaimableFunction.CallAsync<bool>(mobileData.WearLevel);

            return isClaimable;
        }
    }
}