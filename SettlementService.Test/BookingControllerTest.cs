using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SettlementService.Controllers.v1;
using SettlementService.Models;
using SettlementService.Service;

namespace SettlementService.Test
{
    public class BookingControllerTest
    {
        private readonly Mock<IBookingService> _bookingService;

        private readonly Mock<ILogger<BookingController>> _logger;

        public BookingControllerTest()
        {
            _bookingService = new Mock<IBookingService>();
            _logger = new Mock<ILogger<BookingController>>();
        }

        [Fact]
        public async void PostBooking_ShouldReturn_400_OnBeforeHourRequest()
        {
            var bookignRequest = new BookingRequest { BookingTime = "8:00", Name = "James Tom" };
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookignRequest)).Result as BadRequestResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async void PostBooking_ShouldReturn_400_OnAfterHourRequest()
        {
            var bookignRequest = new BookingRequest { BookingTime = "17:00", Name = "James Tom" };
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookignRequest)).Result as BadRequestResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async void PostBooking_ShouldReturn_400_OnInvalidTimeFormatRequest()
        {
            var bookignRequest = new BookingRequest { BookingTime = "1700", Name = "James Tom" };
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookignRequest)).Result as BadRequestResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async void PostBooking_ShouldReturn_400_OnEmptyNameRequest()
        {
            var bookignRequest = new BookingRequest { BookingTime = "12:00", Name = "" };
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookignRequest)).Result as BadRequestResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async void PostBooking_ShouldReturn_200_OnSuccess()
        {
            var bookinglist = GetBookingItemData();
            var bookingRequest = new BookingRequest { BookingTime = "9:00", Name = "John Bob" };
            _bookingService.Setup(x => x.CreateBookingAsync(bookinglist[0])).ReturnsAsync(bookinglist[0].BookingId);
            _bookingService.Setup(x => x.GetBookings(bookinglist[0].BookingTime)).Returns(bookinglist);
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookingRequest)).Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void PostBooking_ShouldReturn_BookingId_OnSuccess()
        {
            var newGuid = Guid.NewGuid();
            var bookingItem = new BookingItem { BookingId = newGuid, BookingTime = new TimeOnly(9, 00), Name = "John Bob" };
            var bookingRequest = new BookingRequest { BookingTime = "9:00", Name = "John Bob" };
            _bookingService.Setup(x => x.CreateBookingAsync(bookingItem));
            _bookingService.Setup(x => x.GetBookings(new TimeOnly(9, 00))).Returns(new List<BookingItem>());
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookingRequest)).Result as OkObjectResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void PostBooking_ShouldReturn_409_OnBookingFull()
        {
            var bookinglist = GetBookingItemData();
            bookinglist.Add(new BookingItem { BookingId = Guid.NewGuid(), BookingTime = new TimeOnly(9, 00), Name = "Ben Brook" });
            var bookingRequest = new BookingRequest { BookingTime = "9:30", Name = "George Sun" };
            _bookingService.Setup(x => x.CreateBookingAsync(bookinglist[0])).ReturnsAsync(bookinglist[0].BookingId);
            _bookingService.Setup(x => x.GetBookings(new TimeOnly(9, 30))).Returns(bookinglist);
            var bookingController = new BookingController(_logger.Object, _bookingService.Object);
            var result = (await bookingController.PostBooking(bookingRequest)).Result as ConflictResult;
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
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
    }
}