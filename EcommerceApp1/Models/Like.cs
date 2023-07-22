using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class Like
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }
        [ForeignKey("Reviews")]
        public int ReviewID { get; set; }
        [ForeignKey("Comments")]
        public int CommentID { get; set; }

    }
}
