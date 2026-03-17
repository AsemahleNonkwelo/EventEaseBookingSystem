using System.ComponentModel.DataAnnotations;

namespace EventEaseBooking.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public int VenueId { get; set; }

        public string ImageUrl { get; set; }

        public Venue Venue { get; set; }
    }
}