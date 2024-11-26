using System.Text.Json.Serialization;

namespace TicketReservationSystem.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Cancelled,
        Expired,
        Refunded
    }
}
