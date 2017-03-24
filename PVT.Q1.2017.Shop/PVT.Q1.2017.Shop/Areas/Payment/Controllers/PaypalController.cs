using Shop.BLL.Services;
using Shop.BLL.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    public class PaypalController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaypalController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        // GET: Payment/Paypal
        public ActionResult Index()
        {
            return View();
        }

        // GET: Payment/Paypal
        public ActionResult PaymentWithCreditCard()
        {
            var viewName = _paymentService.CreatePaymentWithCreditCard();
            return Content(viewName); // View("SuccessView");
        }

        public ActionResult PaymentWithPaypal()
        {
            var content = _paymentService.PaymentWithPaypal(this.Request, this.Session);

            if (content.StartsWith("http"))
            {
                return Redirect(content);
            }
                
            return Content(content);
        }
    }
}