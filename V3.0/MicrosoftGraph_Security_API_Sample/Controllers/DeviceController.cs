using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Authorize]
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IGraphServiceProvider _graphServiceProvider;
        private IGraphService _graphService;
        private IMemoryCacheHelper _memoryCacheHelper;

        public DeviceController(IGraphServiceProvider graphServiceProvider, IMemoryCacheHelper memoryCacheHelper)
        {
            _graphServiceProvider = graphServiceProvider;
            _memoryCacheHelper = memoryCacheHelper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNCDeviceList()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var result = await _graphService.GetNonCompliantDevicesListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDeviceComplianceDetails()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);

                var result = await _graphService.GetDeviceComplianceForChartAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
