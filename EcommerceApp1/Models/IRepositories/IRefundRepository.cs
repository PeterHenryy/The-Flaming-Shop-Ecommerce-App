using System.Collections;
using System.Collections.Generic;

namespace EcommerceApp1.Models.IRepositories
{
    public interface IRefundRepository
    {
        bool Create(Refund refund);
        bool Update(Refund refund);
        IEnumerable<Refund> GetUserRefunds(int userID);
        IEnumerable<Refund> GetCompanyRefunds(int companyID);
        Refund GetRefundByID(int refundID);
    }
}
