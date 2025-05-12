using Austistic.Core.DTOs.Request.Symbol;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Austistic.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/symbol")]
    [ApiController]
    public class SymbolController : ControllerBase
    {
        private readonly ISymbolService _symbolService;

        public SymbolController(ISymbolService symbolService)
        {
            _symbolService = symbolService;
        }
        
        [HttpPost("category/create")]
        public async Task<IActionResult> CreateCategory(CreateSymbolCategory req)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var result = await _symbolService.CreateCatgory(userid, req.CategoryType, req.CategoryName);
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
        [HttpPost("category/retrieve_all_symbol")]
        public async Task<IActionResult> CreateCategory(CatSymbol req)
        {

            var result = await _symbolService.GetAllSymbolIncat(req.catid);
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
        [HttpPost("upload")]
        public async Task<IActionResult> UploadSymbolCategory(UploadImagReq req)
        {

            var result = await _symbolService.UploadSymbolCategory(req.CategoryName, req.file, req.Description);
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
        [AllowAnonymous]
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetSymbolImageByte(string id)
        {

            var result = await _symbolService.GetSymbolImageByte(id);
            if (result.StatusCode == 200)
            {
                return File(result.Result.ImageData, result.Result.ContentType);
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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSymbol(string id)
        {

            var result = await _symbolService.DeleteSymbol(id);
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
        [HttpDelete("cat/delete/{id}")]
        public async Task<IActionResult> DeleteCat(string id)
        {

            var result = await _symbolService.DeleteCat(id);
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
       
        [HttpGet("category/all")]
        public async Task<IActionResult> GetAllcat()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var result = await _symbolService.GetAllcat(userid);
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
