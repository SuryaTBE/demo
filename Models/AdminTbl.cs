

using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class AdminTbl
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
