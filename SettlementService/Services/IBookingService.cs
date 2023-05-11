using SettlementService.Models;

namespace SettlementService.Service
{
    public interface IBookingService
    {
        Task<Guid> CreateBookingAsync(BookingItem bookingItem);
        List<BookingItem> GetBookings(TimeOnly bookingTime);
    }
}
