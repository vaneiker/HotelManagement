namespace HotelManagement.Domain.Enums
{
    public class Enums
    {
        public enum ReservationStatus
        {
            Pending,
            Confirmed,
            CheckedIn,
            CheckedOut,
            Cancelled,
            NoShow
        }

        public enum PaymentStatus
        {
            Pending,
            Completed,
            Cancelled,
            Refunded
        }

        public enum PaymentMethod
        {
            Cash,
            Card,
            Transfer,
            Bank,
            Other
        }
    }
}
