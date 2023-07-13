using System.Collections;
using System.Collections.Generic;

namespace EcommerceApp1.Models.IRepositories
{
    public interface ICreditCardRepository
    {
        bool Create(CreditCard card);
        bool Update(CreditCard card);
        bool Delete(int cardID);
        CreditCard GetCreditCardByID(int cardID);
        IEnumerable<CreditCard> GetSpecificUserCards(int userID);

    }
}
