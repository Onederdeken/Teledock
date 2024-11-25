using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.dbContext;
using Teledock.Models;

namespace Teledock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService) {
            this._clientService = clientService;
        }
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients() {
            var result = await _clientService.getAllClients();
            if (result.Error == String.Empty) {
                return Ok(result.clients);
            }
            else {
                return BadRequest(result.Error);
            }
        }
        [HttpGet("GetClientById")]
        public async Task<IActionResult> GetClientById(int Id)
        {
            var result = await _clientService.getClientById(Id);
            if (result.Error == String.Empty) return Ok(result.client);
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

        [HttpPost("AddIPClient")]
        public async Task<IActionResult> AddIPClient( [Required]ClientIPCommand client)
        {
            client.setTypeClient(TypeClient.IP);
            var result = await _clientService.addClientIP(client);
            if(result.code == 400)
            {
                return BadRequest(result.Message);
            }
            else return Ok(result.Message);
        }
        [HttpPost("AddULClient")]
        public async Task<IActionResult> AddULClient([Required] ClientULCommand client)
        {
            client.setTypeClient(TypeClient.UL);
            var result = await _clientService.addClientUL(client, client.founders);
            if(result.code == 400)return BadRequest(result.Message);
            else return Ok(result.Message);
        }
        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int IdClient)
        {
            var result = await _clientService.DeleteClient(IdClient);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpDelete("DeleteFounder")]
        public async Task<IActionResult> DeleteFounder(int IdFounder)
        {
            var result = await _clientService.DeleteClient(IdFounder);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("ClientIPUpdate")]
        public async Task<IActionResult> UpdateClientIP(ClientIPCommand clientCommand)
        {
            var result = await _clientService.UpdateClient(clientCommand);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("ClientULUpdate")]
        public async Task<IActionResult> UpdateClientUL(ClientULCommand clientCommand)
        {
            var result = await _clientService.UpdateClient(clientCommand, clientCommand.founders);
            if (result.code == 200) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPut("FounderUpdate")]
        public async Task<IActionResult> UpdateFounder(FounderCommand founder)
        {
            var result = await _clientService.UpdateFounder(founder);
            if(result.code == 200)return Ok(result.Message);
            else return BadRequest(result.Message);
        }

    }
}