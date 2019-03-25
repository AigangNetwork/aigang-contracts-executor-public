using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aigang.Contracts.Executor.Utils;
using log4net;
using Newtonsoft.Json;

namespace Aigang.Ethernscan.Client
{
    public static class EtherscanClient
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(EtherscanClient));
        private static string token;
        private static readonly HttpClient _httpClient = GetHttpClient();
        
        public static async Task<string> GetABIAsync(string address)
        {
            //https://api.etherscan.io
            var requestPath = $"api?module=contract&action=getabi&address={address}&apikey={token}";

            _logger.Info($"Requesting to to get abi");

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_httpClient.BaseAddress + requestPath),
                Method = HttpMethod.Get
                //Content = stringContent
            };

            //httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await _httpClient.SendAsync(httpRequestMessage);
            var response =  await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                _logger.Error($"Communication failed with Etherscan Api: StatusCode: {result.StatusCode} Reason: {response}");
                HttpClientService.ThrowHandledException(result.StatusCode, response);
            }
            
            EtherscanResponse deserialized = JsonConvert.DeserializeObject<EtherscanResponse>(response);

            if (deserialized.Status == 1 && deserialized.Message.Equals("OK"))
            {
                return deserialized.Result;
            }
            
            return string.Empty;
        }
        
        private static HttpClient GetHttpClient()
        {
            var client = new HttpClient();

            var baseAddress = ConfigurationManager.GetString("Etherscan:BaseAddress");
            token = ConfigurationManager.GetString("Etherscan:Token");

            client.BaseAddress = new Uri(baseAddress);
          
            return client;
        }
    }
}