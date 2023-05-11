using System.ComponentModel.DataAnnotations;

namespace SettlementService.Models
{
    public class BookingItem
    {
        [Key]
        public Guid BookingId { get; set; }
        public TimeOnly BookingTime { get; set; }
        public string Name { get; set; } = null!;
    }
}
