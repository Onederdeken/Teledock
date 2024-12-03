using System.ComponentModel.DataAnnotations.Schema;
using Teledock.Domain.Enums;

namespace Teledock.Domain.Models
{
    [Table("клиенты")]
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id{get;set;}
        [Column("Инн")]
        public String Inn{get;set;}
        [Column("имя")]
        public String Name{get;set;}
        [Column("тип")]
        public TypeClient _TypeClient{get;set;}
        [Column("дата добавления")]
        public DateTime dateAdd{get;set;}
        [Column("дата обновления")]
        public DateTime? dateUpdate{get;set;}
        public List<Founder> founders{get;set;}
        
        
    }
}