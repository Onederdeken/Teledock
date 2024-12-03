
using System.Runtime.Serialization;

namespace Teledock.Domain.Enums
{
    public enum TypeClient
    {
        [EnumMember(Value = "UL")]
        UL = 1,
        [EnumMember(Value = "IP")]
        IP = 2
    }
}
