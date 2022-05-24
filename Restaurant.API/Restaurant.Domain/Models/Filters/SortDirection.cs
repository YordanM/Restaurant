using System.Runtime.Serialization;

namespace Restaurant.Domain.Models
{
    public enum SortDirection
    {
        [EnumMember(Value = "Ascending")]
        Ascending,
        [EnumMember(Value = "Descending")]
        Descending
    }
}
