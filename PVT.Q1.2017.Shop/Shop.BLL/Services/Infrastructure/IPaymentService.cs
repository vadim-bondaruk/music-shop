﻿namespace Shop.BLL.Services.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// The payment service interface
    /// </summary>
    public interface IPaymentService
    {

        string CreatePaymentWithCreditCard();

        string PaymentWithPaypal(HttpRequestBase Request, HttpSessionStateBase Session);

    }
}
