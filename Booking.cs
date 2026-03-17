using System.ComponentModel.DataAnnotations;

namespace EventEaseBooking.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int EventId { get; set; }
        public int VenueId { get; set; } // KEEP THIS

        public DateTime BookingDate { get; set; }

        public Event Event { get; set; }
        public Venue Venue { get; set; }
    }
}
