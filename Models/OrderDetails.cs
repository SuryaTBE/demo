using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int MovieId { get; set; }
        public int OrderMasterId { get; set; }
        public int NoOfTickets { get; set; }
        public int Cost { get; set; }
        public virtual OrderMasterTbl? OrderMasterTbl { get; set; }
        public virtual BookingTbl? BookingTbl { get; set; }

    }
}
