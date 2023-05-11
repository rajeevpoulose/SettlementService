using SettlementService.DataContext;
using SettlementService.Entities;
using SettlementService.Models;

namespace SettlementService.Service
{
    public class BookingService : IBookingService
    {
        private readonly BookingContext _Context;
        public BookingService(BookingContext context)
        {
            _Context = context;
        }
        public async Task<Guid> CreateBookingAsync(BookingItem bookingItem)
        {
            _Context.BookingItems.Add(bookingItem);
            await _Context.SaveChangesAsync();
            return bookingItem.BookingId;
        }

        public List<BookingItem> GetBookings(TimeOnly bookingTime)
        {
            var bookedSlots = (from item in _Context.BookingItems
                                    let currentBokingStartTime = item.BookingTime
                                    let currentBookingEndTime = currentBokingStartTime.AddMinutes(Constants.MEETING_DURATION_IN_MINUTS)
                                    let newBookingStartTime = bookingTime
                                    let newBookingEndTime = newBookingStartTime.AddMinutes(Constants.MEETING_DURATION_IN_MINUTS)
                                    where currentBokingStartTime <= newBookingEndTime && currentBookingEndTime >= newBookingStartTime
                                    select item);
            return bookedSlots.ToList();
        }
    }
}
