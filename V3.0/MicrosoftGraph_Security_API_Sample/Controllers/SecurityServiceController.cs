using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosoftGraph_Security_API_Sample.Models.Responses;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;

namespace MicrosoftGraph_Security_API_Sample.Controllers
{
    [Authorize]
    [Route("api/security")]
    [ApiController]
    public class SecurityServiceController : ControllerBase
    {
        private readonly ISecurityServiceProvider _securityServiceProvider;
        private readonly IGraphServiceProvider _graphServiceProvider;
        private ISecurityService _securityService;
        private IGraphService _graphService;

        public SecurityServiceController(ISecurityServiceProvider securityServiceProvider,
                                         IGraphServiceProvider graphServiceProvider)
        {
            _securityServiceProvider = securityServiceProvider;
            _graphServiceProvider = graphServiceProvider;
        }

        [HttpGet("[action]")]
        public IActionResult GetNewToken()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _securityService = _securityServiceProvider.GetService(token);
                string newToken = _securityService.GetAccessToken();

                return Ok(newToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSoftwareInventoryAsync()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _securityService = _securityServiceProvider.GetService(token);
                var result = await _securityService.GetSoftwareInventoryResponseAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDeviceScoreAsync()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _securityService = _securityServiceProvider.GetService(token);
                var result = await _securityService.GetDeviceScoreAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWindowsUpdateStatus()
        {
            try
            {
                var token = string.Empty;

                if (Request.Headers.ContainsKey("Authorization"))
                {
                    token = Request.Headers["Authorization"].ToString()?.Split(" ")?[1];
                }

                _graphService = _graphServiceProvider.GetService(token);
                var result = await _graphService.GetWindowsUpdateStatesForChartAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
