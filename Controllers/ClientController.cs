using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teledock.Abstractions;
using Teledock.dbContext;

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
        [HttpGet("GetClient")]
        public async  Task<IActionResult> GetAllUsers(){
            var result = await _clientService.getAllClients();
            if(result.Error == String.Empty){
                return Ok(result.clients);
            }
            else{
                return BadRequest(result.Error);
            }
            
        }
    }
}