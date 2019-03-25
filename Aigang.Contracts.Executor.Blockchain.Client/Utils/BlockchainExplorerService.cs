using System.Numerics;
using System.Threading.Tasks;
using Aigang.Contracts.Executor.Utils;
using Nethereum.StandardTokenEIP20;
using Nethereum.Web3;

namespace Aigang.Contracts.Executor.Blockchain.Client.Utils
{
    public static class BlockchainExplorerService
    {
        public static async Task<decimal> GetEthBalance(string address)
        {
            var web3 = new Web3(ConfigurationManager.GetString("NetworkProvider"));
            var weiBalance = await web3.Eth.GetBalance.SendRequestAsync(address);
            var balance = Web3.Convert.FromWei(weiBalance);

            return balance;
        }
        
        public static async Task<decimal> GetTokenBalance(string address)
        {
            var web3 = new Web3(ConfigurationManager.GetString("NetworkProvider"));
            var tokenAddress = ConfigurationManager.GetString("TokenAddress");
            
            var service = new StandardTokenService(web3, tokenAddress);
            
            BigInteger wei = await service.BalanceOfQueryAsync(address);
            
            var balance = Web3.Convert.FromWei(wei);

            return balance;
        }
    }
}