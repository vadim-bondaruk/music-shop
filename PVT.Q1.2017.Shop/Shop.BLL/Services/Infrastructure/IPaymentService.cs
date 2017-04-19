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

    /// <summary>
    /// The payment service interface
    /// </summary>
    public interface IPaymentService
    {

        string CreatePaymentWithCreditCard();

        string PaymentWithPaypal(HttpRequestBase Request, HttpSessionStateBase Session, CartViewModel cart = null);

        string PaymentWithPaypalDemo(HttpRequestBase Request, HttpSessionStateBase Session);

        void CreatePaymentTransaction(CartViewModel cart);

        IEnumerable<PaymentTransaction> GetTransactionsByUserId(int userID);
    }
}
