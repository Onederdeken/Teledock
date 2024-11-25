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
        public ClientController(IClientService clientService){
            this._clientService = clientService;
        }
        [HttpGet("GetAllClients")]
        public async  Task<IActionResult> GetAllClients(){
            var result = await _clientService.getAllClients();
            if(result.Error == String.Empty){
                return Ok(result.clients);
            }
            else{
                return BadRequest(result.Error);
            }
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
    }
}