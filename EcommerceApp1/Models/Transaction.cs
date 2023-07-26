using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp1.Models
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }
        public double Total { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public int QuantityBought { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PaymentType { get; set; }
        public string CouponCode { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public virtual Product CurrentProduct { get; set; }
        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }
        
    }
}
