using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SettlementService.Models;
using SettlementService.Service;

namespace SettlementService.Controllers.v1
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BookingController : ControllerBase
    {

        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;
        public BookingController(ILogger<BookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult<BookingResponse>> PostBooking(BookingRequest bookingRequest)
        {
            var bookingStartTime = new TimeOnly(09, 00);
            var bookingEndTime = new TimeOnly(16, 00);
            try
            {
                var isValidTimeFormat = TimeOnly.TryParse(bookingRequest.BookingTime, out var boookingTime);
                var isBusinessHour = boookingTime >= bookingStartTime && boookingTime <= bookingEndTime;

                if (isValidTimeFormat && isBusinessHour && !string.IsNullOrEmpty(bookingRequest.Name))
                {
                    if (_bookingService.GetBookings(boookingTime).Count < Entities.Constants.MAX_SIMULTANEOUS_BOOKINGS)
                    {
                        BookingItem bookingItem = new()
                        {
                            BookingId = Guid.NewGuid(),
                            BookingTime = boookingTime,
                            Name = bookingRequest.Name
                        };
                        var newBookingId = await _bookingService.CreateBookingAsync(bookingItem);
                        return new OkObjectResult(new BookingResponse() { BookingId = newBookingId });
                    }
                    else { return Conflict(); }
                }
                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {
                _logger.Log(LogLevel.Error, Ex.Message);
                return StatusCode(500);
            }
        }
    }
}