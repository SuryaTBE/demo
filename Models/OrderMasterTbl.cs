using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Models
{
    public class OrderMasterTbl
    {
        [Key]
        public int OrderMasterId { get; set; }
        [DataType(DataType.Date)]
        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CardNo { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
        public virtual UserTbl? user { get; set; }

        public virtual ICollection<OrderDetails> Details { get; set; }


    }
}
