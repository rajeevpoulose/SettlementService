using Microsoft.EntityFrameworkCore;
using SettlementService.Models;

namespace SettlementService.DataContext
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options)
      : base(options)
        {
        }
        public DbSet<BookingItem> BookingItems { get; set; } 
    }
}
