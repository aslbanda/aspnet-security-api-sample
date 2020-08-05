using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using MicrosoftGraph_Security_API_Sample.Services;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public class SecurityServiceProvider : ISecurityServiceProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AzureConfiguration _azureConfiguration;

        public SecurityServiceProvider(AzureConfiguration azureConfiguration,
                                        IHttpClientFactory httpClientFactory)
        {
            _azureConfiguration = azureConfiguration;
            _httpClientFactory = httpClientFactory;
        }

        public ISecurityService GetService(string token)
        {
            return new SecurityService(_azureConfiguration, token, _httpClientFactory);
        }
    }
}
