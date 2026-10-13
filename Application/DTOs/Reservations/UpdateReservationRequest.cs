namespace HotelManagement.Application.DTOs.Reservations
{
    public class UpdateReservationRequest
    {
        public int PropertyId { get; set; }
        public int UnitId { get; set; }
        public long GuestId { get; set; }
        public string ReservationNumber { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Notes { get; set; }
    }
}
