using System.Text.Json.Serialization;

namespace ApiProject.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage,
        Cleric

    }
}
