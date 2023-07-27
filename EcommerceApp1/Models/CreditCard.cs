using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class CreditCard
    {
        [Key]
        public int ID { get; set; }
        public string CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public int CVV { get; set; }
        public DateTime Expiry { get; set; }

        [ForeignKey("AspNetUsers")]
        public int? UserID { get; set; }

    }
}
