using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teledock.Models
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
        public DateOnly dateAdd{get;set;}
        [Column("дата обновления")]
        public DateOnly? dateUpdate{get;set;}

        public int ClientId{get;set;}
        public Client client {get;set;}
    }
}