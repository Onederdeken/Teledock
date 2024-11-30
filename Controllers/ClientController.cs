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

namespace Teledock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService, IMediator mediator) {
            this._clientService = clientService;
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
            var result = await _clientService.getIPClients();
            if (result.Error == String.Empty) return Ok(result.clients);
            else return BadRequest(result.clients);
        }
        [HttpGet("GetClientsUL")]
        public async Task<IActionResult> GetClientsUL()
        {
            var result = await _clientService.getULClients();
            if (result.Error == String.Empty) return Ok(result.clients);
            else return BadRequest(result.clients);
        }

        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient( [Required]ClientCommand client, [Required]TypeClient type)
        {
            client.setTypeClient(type);
            var result = await _clientService.addClient(client);
            if(result.code == 400)
            {
                return BadRequest(result.Message);
            }
            else return Ok(result.Message);
        }
        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClient([Required]int IdClient)
        {
            var result = await _clientService.DeleteClient(IdClient);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("ClientUpdate")]
        public async Task<IActionResult> UpdateClient([Required]ClientCommand clientCommand, [Required]int clientID, TypeClient type)
        {
            clientCommand.setTypeClient(type);
            var result = await _clientService.UpdateClient(clientCommand, clientID);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
    }
}