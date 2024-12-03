using System.ComponentModel.DataAnnotations.Schema;

namespace Teledock.Domain.Models
{
    
    [Table("учредители")]
    public class Founder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{get;set;}
        [Column("Инн")]
        public String Inn{get;set;}
        [Column("фио")]
        public String FIO{get;set;}
         [Column("дата добавления")]
        public DateTime dateAdd{get;set;}
        [Column("дата обновления")]
        public DateTime? dateUpdate{get;set;}

        public int ClientId{get;set;}
        public Client client {get;set;}
    }
}