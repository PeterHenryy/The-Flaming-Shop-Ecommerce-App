using System.Collections.Generic;

namespace EcommerceApp1.Models.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public List<CreditCard> UserCards { get; set; }
        public int? ChosenCardID { get; set; }
    }
}
