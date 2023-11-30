using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp1.Models.ViewModels
{
    public class UserTransactionsViewModel
    {
        public IEnumerable<Transaction> UserTransactions { get; set; }
        public IEnumerable<Refund> UserRefunds { get; set; }

    }
}
