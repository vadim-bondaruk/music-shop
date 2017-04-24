namespace Shop.BLL.Services.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Common.ViewModels;
    using Common.Models;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// The payment service interface
    /// </summary>
    public interface IPaymentService
    {

        string PaymentWithPaypal(HttpRequestBase request, HttpSessionStateBase session, CartViewModel cart = null);
        
        void CreatePaymentTransaction(CartViewModel cart);

        IEnumerable<PaymentTransaction> GetTransactionsByUserId(int userID);

        PagedResult<PaymentTransaction> GetDataPerPage(int? userID, int pageNumber = 1, int count = 10);

        PayResultsViewModel GetSellerPays(int userID, int currencyID);

        PayResultsViewModel GetAllPays(int currencyID);
    }
}
