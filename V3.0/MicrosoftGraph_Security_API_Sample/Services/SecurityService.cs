using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using MicrosoftGraph_Security_API_Sample.Models.DomainModels;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Services
{
    public class SecurityService : ISecurityService
    {
        IHttpClientFactory _httpClientFactory;

        public SecurityService(AzureConfiguration azureConfig, string jwtToken, IHttpClientFactory httpClientFactory)
        {
            AccessToken = SDKHelper.GetAccessToken(azureConfig, jwtToken, Models.Enums.AccessTokenScopes.SecurityApi);
            _httpClientFactory = httpClientFactory;
        }

        public string AccessToken { get; set; }

        public string GetAccessToken()
        {
            return AccessToken;
        }

        public async Task<DeviceScoreResponse> GetDeviceScoreAsync()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

                string url = $"https://api.securitycenter.windows.com/api/ConfigurationScore";
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                var responseMessage = await httpClient.SendAsync(request);
                if(responseMessage != null && responseMessage.IsSuccessStatusCode)
                {
                    var responseBody = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
                    double score = (double)responseBody["score"];

                    DeviceScoreResponse deviceScoreResponse = new DeviceScoreResponse
                    {
                        DeviceScore = score,
                        TotalScore = 750.0,
                        DeviceScorePercentage = (score / 750.0) * 100
                    };

                    return deviceScoreResponse;
                }

                return null;
            }
        }

        public async Task<IList<SoftwareInventoryResponse>> GetSoftwareInventoryResponseAsync()
        {
            IList<SoftwareInventoryResponse> softwareInventoryResponses = new List<SoftwareInventoryResponse>();

            IList<Software> softwares = await GetSoftwares();

            var top10Softwares = softwares.OrderByDescending(p => p.ImpactScore).Take(10);
            var machineReferences = await GetMachineReference(top10Softwares);
            if(machineReferences != null)
            {
                foreach (var reference in machineReferences)
                {
                    string exposedDevices = $"{reference.Key.ExposedMachines}/{reference.Value.Count}";

                    softwareInventoryResponses.Add(new SoftwareInventoryResponse
                    {
                        Name = reference.Key.Name,
                        Vendor = reference.Key.Vendor,
                        Weaknesses = reference.Key.Weaknesses,
                        ImpactScore = reference.Key.ImpactScore,
                        ExposedDevices = exposedDevices
                    });
                }
            }            

            return softwareInventoryResponses;
        }

        private async Task<Dictionary<Software, IList<JObject>>> GetMachineReference(IEnumerable<Software> top10Software)
        {
            Dictionary<Software, IList<JObject>> softwareMachineReference = new Dictionary<Software, IList<JObject>>();

            foreach (var software in top10Software)
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

                    string url = $"https://api.securitycenter.windows.com/api/Software/{software.Id}/machineReferences";
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                    var responseMessage = await httpClient.SendAsync(request);
                    if (responseMessage != null && responseMessage.IsSuccessStatusCode)
                    {
                        var objectString = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
                        var result = objectString["value"].ToObject<IList<JObject>>();

                        softwareMachineReference.Add(software, result);
                    }                    
                }
            }

            return softwareMachineReference;
        }

        private async Task<IList<Software>> GetSoftwares()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

                string url = $"https://api.securitycenter.windows.com/api/Software";
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                var responseMessage = await httpClient.SendAsync(request);
                if (responseMessage != null && responseMessage.IsSuccessStatusCode)
                {
                    var objectString = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());

                    var result = objectString["value"].ToObject<IList<Software>>();

                    return result;
                }

                return null;
            }
        }
    }
}
