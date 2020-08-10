using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetrieveDeviceScoreApp.Models;

namespace RetrieveDeviceScoreApp
{
    public class RetrieveDeviceScoreFunc
    {
        private System.Net.Http.IHttpClientFactory _httpClientFactory;

        public RetrieveDeviceScoreFunc(System.Net.Http.IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [FunctionName("RetrieveDeviceScore")]
        public void Run([TimerTrigger("0 0 0 * * *")]TimerInfo myTimer, ILogger log)
        {
            string accessToken = GetAccessTokenAsync(_httpClientFactory.CreateClient()).Result;

            JObject response = RetrieveDeviceScoreAsync(accessToken, _httpClientFactory.CreateClient()).Result;

            StoreDeviceScore(response);
        }

        private async void StoreDeviceScore(JObject response)
        {
            if(response != null)
            {
                double score = (double)response["score"];
                DeviceScoreResponse deviceScoreResponse = new DeviceScoreResponse
                {
                    DeviceScore = score
                };

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=securitydashboardstorage;AccountKey=JwdJB7A1TD0lFZxgPQxHLV6BtxdITxDEe3Qk/8Z0uK0VL1HGaZz4g4kfW4Nxw0iKmRt3Rtl8thCJBdsyQhVMFw==;EndpointSuffix=core.windows.net");

                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(deviceScoreResponse);

                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Execute the operation.
                var table = tableClient.GetTableReference("DeviceScoreInfo");

                _ = await table.ExecuteAsync(insertOrMergeOperation);
            }
        }

        private async Task<JObject> RetrieveDeviceScoreAsync(string accessToken, HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            string url = $"https://api.securitycenter.windows.com/api/ConfigurationScore";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            var responseMessage = await httpClient.SendAsync(request);
            if (responseMessage != null && responseMessage.IsSuccessStatusCode)
            {
                var responseBody = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());

                return responseBody;
            }

            return null;
        }

        private async static Task<string> GetAccessTokenAsync(HttpClient httpClient)
        {
            var body = new List<KeyValuePair<string, string>>();
            body.Add(new KeyValuePair<string, string>("resource", "https://securitycenter.onmicrosoft.com/windowsatpservice"));
            body.Add(new KeyValuePair<string, string>("client_id", "28535f1e-7b9d-4a93-9be2-e11d2d6412f0"));
            body.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            body.Add(new KeyValuePair<string, string>("client_secret", "cu925Ubf8Yq6HlCd9DJXH.DLyl-.k-3xnj"));

            string url = string.Format("https://login.microsoftonline.com/{0}/oauth2/token", "d1fd9326-4644-4e1c-8685-279bfbe2f5a0");

            var response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(body));

            if (!response.IsSuccessStatusCode) return null;
            response.EnsureSuccessStatusCode();

            var resultstring = await response.Content.ReadAsStringAsync();
            var resultJson = JsonConvert.DeserializeObject<JObject>(resultstring);

            return resultJson["access_token"].Value<string>();
        }
    }
}
