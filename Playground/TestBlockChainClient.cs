using Aigang.Contracts.Executor.Blockchain.Client.Contracts;
using Aigang.Contracts.Executor.Utils;
using Aigang.Ethernscan.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playground
{
    [TestClass]
    public class TestBlockChainClient
    {
        [TestInitialize]
        public void TestInitialize()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            ConfigurationManager.Configuration = builder.Build();
        }
        
     
        [TestMethod]
        public void GetDetailsTest()
        {
            var address = "0xFB205BFAF7D129f35f37eA5556A118ED3EE5cBf9";
            var abi =  EtherscanClient.GetABIAsync(address).Result;
            var product = new InsuranceProduct(address, abi);

            var details = product.GetProductAsync().Result;
            
            Assert.IsNotNull(details.Title);
        }
    }
}