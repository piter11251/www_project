using System.Text.Json.Serialization;

namespace TicketReservationSystem.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMethod
    {
        Cash,
        Transfer
    }
}
