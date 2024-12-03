using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;
using Teledock.Queries.Founders;
using Teledock.Services;

namespace Teledock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FounderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FounderController(IMediator mediator,IFounderService founderService)
        {
            this._mediator = mediator;
        }
        [HttpGet("GetAllFounders")]
        public async Task<IActionResult> GetAllFounders()
        {

            var result = await _mediator.Send(new FoundersQueries());
            if (result.Error == String.Empty)
            {
                return Ok(result.FounderResponse);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
        [HttpGet("GetFoundertById")]
        public async Task<IActionResult> GetFounderById([Required]int FounderId)
        {
            var FounderQuery = new FoundersQueries() { Id = FounderId };
            var result = await _mediator.Send(FounderQuery);
            if (result.Error == String.Empty) return Ok(result.FounderResponse);
            else return BadRequest(result.Error);
        }
     

        [HttpPost("AddFounder")]
        public async Task<IActionResult> AddFounder([Required] Founder founder, [Required] int ClientId)
        {
            var FounderCommand = new FounderCommand() {
                Inn = founder.Inn,
                FIO = founder.FIO,
                ClientId = ClientId,
                Command = Command.Add
            };
            var result = await _mediator.Send(FounderCommand);
            if (result.code == 400)
            {
                return BadRequest(result.Message);
            }
            else return Ok(result.Message);
        }
        [HttpDelete("DeleteFounder")]
        public async Task<IActionResult> DeleteFounder([Required]int FounderId)
        {
            var FounderCommand = new FounderCommand()
            {
               Id = FounderId,
                Command = Command.Delete
            };
            var result = await _mediator.Send(FounderCommand);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("FounderUpdate")]
        public async Task<IActionResult> UpdateFounder([Required]Founder founder, [Required]int founderID)
        {
            var FounderCommand = new FounderCommand()
            {
                Id= founderID,
                Inn = founder.Inn,
                FIO = founder.FIO,
                Command = Command.Update
            };
            var result = await _mediator.Send(FounderCommand);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("ChangeClient")]
        public async Task<IActionResult> ChangeClient([SwaggerParameter(Description ="ввод id учредителя которому будем менять клиента", Required =true)]int FounderId,
            [SwaggerParameter(Description = "Ввод id клиента на которого хотим сменить", Required = true)] int ToClientID)
        {
            var FounderCommand = new FounderCommand()
            {
                Id = FounderId,
                ClientId = ToClientID,
                Command = Command.ChangeClient
            };
            var result = await _mediator.Send(FounderCommand);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
    }
}
