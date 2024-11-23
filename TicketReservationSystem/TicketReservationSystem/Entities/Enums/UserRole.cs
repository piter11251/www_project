using System.Text.Json.Serialization;

namespace TicketReservationSystem.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}
