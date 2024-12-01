using System.ComponentModel.DataAnnotations;

namespace Teledock.Requests
{
    public class FounderRequest
    {
        [RegularExpression(@"^\d{10,12}$", ErrorMessage = "ИНН должен содержать только цифры и иметь длину от 10 до 12 знаков.")]

        public String Inn { get; set; }

        public String FIO { get; set; }

       
    }
}
