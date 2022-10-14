﻿using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class UserTbl
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public virtual ICollection<BookingTbl> BookingTbls { get; set; }
        public virtual ICollection<OrderMasterTbl> OrderMasterTbls { get; set; }
    }
}