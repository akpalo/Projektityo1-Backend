using Microsoft.EntityFrameworkCore;

namespace VarausJarjestelma.Models
{
    public class ReservationContext:DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options)
           : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Item> Image { get; set; } = null!;
        public DbSet<Item> Reservation { get; set; } = null!;
        public DbSet<Item> User { get; set; } = null!;
    }
}
