using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
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
        public DateTime dateAdd{get;set;}
        [Column("дата обновления")]
        public DateTime? dateUpdate{get;set;}
        public List<Founder> founders{get;set;}
        
        
    }

    [SwaggerSchema(Description = "Тип клиента")]
    
    public enum TypeClient{
        [EnumMember(Value = "UL")]
        UL = 1,
        [EnumMember(Value = "IP")]
        IP = 2
    }
}