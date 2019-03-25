using Aigang.Contracts.Executor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aigang.Transactions.Utils;
using Microsoft.Extensions.Configuration;


namespace Playground
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void TestInitialize()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            ConfigurationManager.Configuration = builder.Build();
        }
        
        [Ignore]
        [TestMethod]
        public void Email_Test()
        {
            EmailSender.SendOutOfBalanceEmail("testsubject", "testEmail");
        }
    }
}