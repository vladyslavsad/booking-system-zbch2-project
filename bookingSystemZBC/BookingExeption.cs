namespace bookingSystemZBC
{
    public class BookingExeption : Exception
    {
        public BookingExeption() : base("Unvalid booking data")
        {
        }
        public BookingExeption(string message) : base(message)
        {
        }
        public BookingExeption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
