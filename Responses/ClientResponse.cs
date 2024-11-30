using System.ComponentModel.DataAnnotations;
using Teledock.Models;

namespace Teledock.Responses
{
    public class ClientResponse
    {
        public int Id { get; set; }
        [RegularExpression(@"^\d{10,12}$", ErrorMessage = "ИНН должен содержать только цифры и иметь длину от 10 до 12 знаков.")]
        public string Inn { get; set; }

        public string Name { get; set; }


        private TypeClient _TypeClient { get; set; }

        public void setTypeClient(TypeClient typeClient)
        {

            _TypeClient = typeClient;
        }
        public TypeClient getTypeClient() { return _TypeClient; }
    }
}
