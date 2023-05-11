using Microsoft.EntityFrameworkCore;
using SettlementService.DataContext;
using SettlementService.Models;
using SettlementService.Service;

namespace SettlementService.Test
{
    public class BookingServiceTest
    {
        [Fact]
        public async void CreateBooking_ShouldReturn_BookingId_OnSuccess()
        {
            var bookingContext = GetBookingContext();
            var newBookingID = Guid.NewGuid();
            var newBookingItem = new BookingItem { BookingId = newBookingID, BookingTime = new TimeOnly(9, 00), Name = "John Bob" };
            var bookingService = new BookingService(bookingContext);
            var res = await bookingService.CreateBookingAsync(newBookingItem);
            Assert.Equal(newBookingID, res);

        }
        [Fact]
        public void GetBookings_ShouldReturn_EmptyList_WhenNoBookings()
        {
            var bookingContext = GetBookingContext();
            var bookingSlot = new TimeOnly(9, 00);
            var bookingService = new BookingService(bookingContext);
            var res = bookingService.GetBookings(bookingSlot);
            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void GetBookings_ShouldReturn_MatchingList_OnValidBookingTime()
        {
            var bookingContext = GetBookingContext();
            var bookingList = GetBookingItemData();
            bookingContext.BookingItems.AddRange(bookingList);
            bookingContext.SaveChanges();
            var bookingSlot = new TimeOnly(9, 00);
            var bookingService = new BookingService(bookingContext);
            var res = bookingService.GetBookings(bookingSlot);
            Assert.NotNull(res);
            Assert.Equal(bookingList, res);
        }
        [Fact]
        public void GetBookings_ShouldReturn_Booking_WhenMeetingTimeOverlap()
        {
            var bookingContext = GetBookingContext();
            var booking = new BookingItem { BookingId = Guid.NewGuid(), BookingTime = new TimeOnly(13, 00), Name = "John Bob" };
            bookingContext.BookingItems.Add(booking);
            bookingContext.SaveChanges();
            var bookingSlot = new TimeOnly(12, 30);
            var bookingService = new BookingService(bookingContext);
            var res = bookingService.GetBookings(bookingSlot);
            Assert.NotNull(res);
            Assert.Equal(booking, res[0]);
        }

        [Fact]
        public void GetBookings_ShouldReturn_EmptyList_WhenNoMeetingTimeOverlap()
        {
            var bookingContext = GetBookingContext();
            var booking = new BookingItem { BookingId = Guid.NewGuid(), BookingTime = new TimeOnly(13, 00), Name = "John Bob" };
            bookingContext.BookingItems.Add(booking);
            bookingContext.SaveChanges();
            var bookingSlot = new TimeOnly(12, 00);
            var bookingService = new BookingService(bookingContext);
            var res = bookingService.GetBookings(bookingSlot);
            Assert.NotNull(res);
            Assert.Empty(res);
        }
        [Fact]
        public void GetBookings_ShouldReturn_EmptyList_OnNoBookingTimeMatch()
        {
            var bookingContext = GetBookingContext();
            var bookingList = GetBookingItemData();
            bookingContext.BookingItems.AddRange(bookingList);
            bookingContext.SaveChanges();
            var bookingSlot = new TimeOnly(10, 00);
            var bookingService = new BookingService(bookingContext);
            var res = bookingService.GetBookings(bookingSlot);
            Assert.NotNull(res);
            Assert.Empty(res);
        }
        private static List<BookingItem> GetBookingItemData()
        {
            var bookingItems = new List<BookingItem>() {
             new BookingItem { BookingId=Guid.NewGuid(), BookingTime=new TimeOnly(9,00), Name="John Bob"  },
             new BookingItem { BookingId=Guid.NewGuid(), BookingTime=new TimeOnly(9,00), Name="James Tom"  },
             new BookingItem { BookingId=Guid.NewGuid(), BookingTime=new TimeOnly(9,00), Name="David Boss"  },
            };
            return bookingItems;
        }
        private static BookingContext GetBookingContext()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
            return new BookingContext(options);
        }
    }
}
