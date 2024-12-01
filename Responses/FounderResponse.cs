using System.ComponentModel.DataAnnotations;

namespace Teledock.Responses
{
    public class FounderResponse
    {
        public int Id { get; set; }
       
        public String Inn { get; set; }

        public String FIO { get; set; }
        public String dateAdd { get; set; }
        public String? dateUpdate { get; set; }
    }
}
