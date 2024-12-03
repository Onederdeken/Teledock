using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.Serialization;

namespace Teledock.Domain.Enums
{

    [SwaggerSchema(Description = "Тип клиента")]

    public enum TypeClient
    {
        [EnumMember(Value = "UL")]
        UL = 1,
        [EnumMember(Value = "IP")]
        IP = 2
    }
}
