
using Microsoft.EntityFrameworkCore;
using demo.Models;

namespace demo.Models
{
    public class SampleContext:DbContext
    {
        public SampleContext() { }
        public SampleContext(DbContextOptions opt):base(opt) { }
        public DbSet<UserTbl> UserTbls { get; set; }
        public DbSet<AdminTbl> AdminTbls { get; set; }
        public DbSet<MovieTbl> MovieTbls { get; set; }
        public DbSet<BookingTbl> BookingTbl { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderMasterTbl> OrderMasterTbls { get; set; }
        
    }
}
