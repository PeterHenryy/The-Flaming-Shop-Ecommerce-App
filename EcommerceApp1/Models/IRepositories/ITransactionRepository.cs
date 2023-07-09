using System.Collections.Generic;

namespace EcommerceApp1.Models.IRepositories
{
    public interface ITransactionRepository
    {
        bool Create(Transaction transaction);
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransactionByID(int transactionID);
    }
}
