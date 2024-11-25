using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Teledock.Models;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Teledock.Commands
{
    public class ClientIPCommand
    {
        [Required]
        required public String Inn{get;set;}
        [Required]
        required public String Name{get;set;}
       
        private TypeClient _TypeClient { get;set;}

        public void setTypeClient(TypeClient typeClient)
        {
            
            this._TypeClient = typeClient;
        }
        public TypeClient getTypeClient() { return this._TypeClient; }

        public Client MapToClient()
        {
            return new Client
            {
                Inn = this.Inn,
                Name = this.Name,
                _TypeClient = TypeClient.IP,
                dateAdd = DateOnly.FromDateTime(DateTime.Now),
                dateUpdate = DateOnly.FromDateTime(DateTime.Now),
            };
        }
       
    }
    
 
    
}