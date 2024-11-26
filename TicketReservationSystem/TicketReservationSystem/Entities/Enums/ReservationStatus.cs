using System.Text.Json.Serialization;

namespace TicketReservationSystem.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Expired
    }
}
