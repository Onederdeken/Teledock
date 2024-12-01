using System.ComponentModel.DataAnnotations;
using Teledock.Models;
using Teledock.Queries.Founders;

namespace Teledock.Requests
{
    public class ClientRequest
    {
        [RegularExpression(@"^\d{10,12}$", ErrorMessage = "ИНН должен содержать только цифры и иметь длину от 10 до 12 знаков.")]
        public String Inn { get; set; }
        public String Name { get; set; }
        private TypeClient Type { get; set; }
        public void setTypeClient(TypeClient typeClient)
        {

            Type = typeClient;
        }
        public TypeClient getTypeClient() { return Type; }
    }
}
