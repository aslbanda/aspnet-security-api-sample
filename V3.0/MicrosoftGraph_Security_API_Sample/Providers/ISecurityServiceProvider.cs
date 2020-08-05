using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Providers
{
    public interface ISecurityServiceProvider
    {
        ISecurityService GetService(string token);
    }
}
