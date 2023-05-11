using System.ComponentModel.DataAnnotations;

namespace SettlementService.Models
{
    public class BookingRequest
    {
        [Required(ErrorMessage = "Booking time is required")]

        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid booking time")]
        public string BookingTime { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
