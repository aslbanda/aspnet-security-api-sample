using MicrosoftGraph_Security_API_Sample.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Services.Interfaces
{
    public interface ISecurityService
    {
        string GetAccessToken();

        Task<IList<SoftwareInventoryResponse>> GetSoftwareInventoryResponseAsync();

        Task<DeviceScoreResponse> GetDeviceScoreAsync();
    }
}
