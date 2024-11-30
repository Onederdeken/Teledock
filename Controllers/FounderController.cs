using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Models;
using Teledock.Services;

namespace Teledock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FounderController : ControllerBase
    {
        private readonly IFounderService _FounderService;
        public FounderController(IFounderService founderService)
        {
            this._FounderService = founderService;
        }
        [HttpGet("GetAllFounders")]
        public async Task<IActionResult> GetAllFounders()
        {
            var result = await _FounderService.getAllFounders();
            if (result.Error == String.Empty)
            {
                return Ok(result.Founders);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
        [HttpGet("GetFoundertById")]
        public async Task<IActionResult> GetFounderById([Required]int FounderId)
        {
            var result = await _FounderService.getFounderById(FounderId);
            if (result.Error == String.Empty) return Ok(result.Founder);
            else return BadRequest(result.Error);
        }
     

        [HttpPost("AddFounder")]
        public async Task<IActionResult> AddIPClient([Required] FounderCommand founder, [Required] int ClientId)
        {
            
            var result = await _FounderService.AddFounder(founder,ClientId);
            if (result.code == 400)
            {
                return BadRequest(result.Message);
            }
            else return Ok(result.Message);
        }
        [HttpDelete("DeleteFounder")]
        public async Task<IActionResult> DeleteClient([Required]int FounderId)
        {
            var result = await _FounderService.DeleteFounder(FounderId);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("FounderUpdate")]
        public async Task<IActionResult> UpdateFounder([Required]FounderCommand founder, [Required]int founderID)
        {
            var result = await _FounderService.UpdateFounder(founder, founderID);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("ChangeClient")]
        public async Task<IActionResult> ChangeClient([SwaggerParameter(Description ="ввод id учредителя которому будем менять клиента", Required =true)]int FounderId,
            [SwaggerParameter(Description = "Ввод id клиента на которого хотим сменить", Required = true)] int ToClientID)
        {
            var result = await _FounderService.ChangeClient(FounderId, ToClientID);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
    }
}
