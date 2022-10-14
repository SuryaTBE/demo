using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class BookingTbl
    {
        [Key]
        public int BookingId { get; set; }
        public int MovieId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        public int NoOfTickets { get; set; }
        public string SeatNo { get; set; }
        public int AmountTotal { get; set; }
        public virtual MovieTbl? Movie { get; set; }
        public virtual UserTbl? User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
