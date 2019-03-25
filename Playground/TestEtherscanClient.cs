using Aigang.Contracts.Executor.Utils;
using Aigang.Ethernscan.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;


namespace Playground
{
    [TestClass]
    public class TestEtherscanClient
    {
        [TestInitialize]
        public void TestInitialize()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            ConfigurationManager.Configuration = builder.Build();
        }
        
     
        [TestMethod]
        public void Etherscan_Test()
        {
            var result =  EtherscanClient.GetABIAsync("0x16ecc82b4E3e5Ff5A4dB8510ED191282A37639B0").Result;
        }
    }
}