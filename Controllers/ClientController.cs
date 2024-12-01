using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.dbContext;
using Teledock.Models;
using Teledock.Queries.Clients;
using Teledock.Requests;

namespace Teledock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator) {
            
            this._mediator = mediator;
        }
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients() {
            ClientsQueries clientsQueries = new ClientsQueries();
            var result = await _mediator.Send(clientsQueries);
            if (result.Error == String.Empty) {
                return Ok(result.ClientQueries);
            }
            else {
                return BadRequest(result.Error);
            }
        }
        [HttpGet("GetClientById")]
        public async Task<IActionResult> GetClientById([Required] int Id)
        {
            ClientQuery clientQuery = new ClientQuery()
            {
                Id = Id
            };
            var result = await _mediator.Send(clientQuery);
            if (result.Error == String.Empty) return Ok(result.ClientQuery);
            else return BadRequest(result.Error);
        }
        [HttpGet("GetClientsIP")]
        public async Task<IActionResult> GetClientsIP()
        {
            ClientsQueries clientQueries = new ClientsQueries()
            {
                Type = TypeClient.IP
            };
            var result = await _mediator.Send(clientQueries);
            if (result.Error == String.Empty) return Ok(result.ClientQueries);
            else return BadRequest(result.ClientQueries);
        }
        [HttpGet("GetClientsUL")]
        public async Task<IActionResult> GetClientsUL()
        {
            ClientsQueries clientQueries = new ClientsQueries()
            {
                Type = TypeClient.UL
            };
            var result = await _mediator.Send(clientQueries);
            if (result.Error == String.Empty) return Ok(result.ClientQueries);
            else return BadRequest(result.ClientQueries);
        }

        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient( [Required]ClientRequest client, [Required]TypeClient type)
        {
            client.setTypeClient(type);
            ClientCommand command = new ClientCommand()
            {
                Inn = client.Inn,
                Name = client.Name,
                comand = Command.Add,
                _TypeClient = type
            };
            var result = await _mediator.Send(command);
            if(result.code == 400)
            {
                return BadRequest(result.Message);
            }
            else return Ok(result.Message);
        }
        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClient([Required]int IdClient)
        {
            ClientCommand command = new ClientCommand()
            {
                Id = IdClient,
                comand = Command.Delete
            };
            var result = await _mediator.Send(command);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("ClientUpdate")]
        public async Task<IActionResult> UpdateClient([Required]ClientRequest client, [Required]int clientID, TypeClient type)
        {
            client.setTypeClient(type);
            ClientCommand command = new ClientCommand()
            {
                Id = clientID,
                Inn = client.Inn,
                Name = client.Name,
                comand = Command.Update,
                _TypeClient = type
            };
            var result = await _mediator.Send(command);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
    }
}