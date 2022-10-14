
using Microsoft.EntityFrameworkCore;
using demo.Models;

namespace demo.Models
{
    public class SampleContext:DbContext
    {
        public SampleContext() { }
        public SampleContext(DbContextOptions opt):base(opt) { }
        public DbSet<UserTbl> userTbls { get; set; }
        public DbSet<AdminTbl> adminTbls { get; set; }
        public DbSet<MovieTbl> movieTbls { get; set; }
        public DbSet<BookingTbl> BookingTbl { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderMasterTbl> OrderMasterTbls { get; set; }
        
    }
}
