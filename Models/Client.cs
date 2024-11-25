using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Teledock.Models
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
        public DateOnly dateAdd{get;set;}
        [Column("дата обновления")]
        public DateOnly dateUpdate{get;set;}
        public List<Founder> founders{get;set;}
        
    }
  
    public enum TypeClient{
        UL = 1,
        IP = 2
    }
}