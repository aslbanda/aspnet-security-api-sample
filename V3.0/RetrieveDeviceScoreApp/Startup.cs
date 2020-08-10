using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(RetrieveDeviceScoreApp.Startup))]
namespace RetrieveDeviceScoreApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
        }
    }
}
