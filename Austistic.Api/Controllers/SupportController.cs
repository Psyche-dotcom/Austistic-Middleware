using Austistic.Core.DTOs.Request.Support;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Austistic.Api.Controllers
{
    [Route("api/support")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("user/ticket")]
        public async Task<IActionResult> RetrieveUserTicket()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var result = await _supportService.RetrieveUserTicket(userid);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("user/ticket/active/admin")]
        public async Task<IActionResult> RetrieveAllActiveUserTicketForAdmin()
        {
          
            var result = await _supportService.RetrieveAllActiveUserTicketForAdmin();
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("ticket/create")]
        public async Task<IActionResult> CreateTicket(CreateSupportTicket req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var result = await _supportService.CreateTicket(userid, req.Message);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("ticket/message")]
        public async Task<IActionResult> AddNewMessage(SupportMessageReq req)
        {
           
            var result = await _supportService.AddNewMessage(req.Ticketid, req.Message,req.IsAdmin);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("ticket/close")]
        public async Task<IActionResult> CloseTicket(CloseTicketReq req)
        {
           
            var result = await _supportService.CloseTicket(req.Ticketid);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
