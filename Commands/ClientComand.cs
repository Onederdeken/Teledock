using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teledock.Commands
{
    public class ClientComand
    {
        public String Inn{get;set;}
        
        public String Name{get;set;}
        public TypeClient _TypeClient{get;set;}
       
    }
    
    public enum TypeClient{
        UL,
        IP
    }
    
}