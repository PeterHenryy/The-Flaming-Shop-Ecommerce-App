using EcommerceApp1.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string Body { get; set; }

        [ForeignKey("AspNetUsers")]
        public int? UserID { get; set; }
        public virtual AppUser User { get; set; }

        [ForeignKey("Reviews")]
        public int? ReviewID { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }
    }
}
