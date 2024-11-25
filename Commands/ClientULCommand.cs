using System.ComponentModel.DataAnnotations;
using Teledock.Models;

namespace Teledock.Commands
{
    public class ClientULCommand
    {
        [Required]
        public String Inn { get; set; }
        [Required]
        public String Name { get; set; }

        public List<FounderCommand> founders { get; set; }

        private TypeClient _TypeClient { get; set; }

        public void setTypeClient(TypeClient typeClient)
        {

            this._TypeClient = typeClient;
        }
        public TypeClient getTypeClient() { return this._TypeClient; }

    }


}

