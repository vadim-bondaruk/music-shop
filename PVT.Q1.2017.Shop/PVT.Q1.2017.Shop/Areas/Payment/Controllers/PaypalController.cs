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

        public PaypalController(IPaymentService PaymentService)
        {
            this._paymentService = PaymentService;
        }

        // GET: Payment/Paypal
        public ActionResult Index()
        { 
            return View();
        }

        // GET: Payment/Paypal
        public ActionResult PaymentWithCreditCard()
        {
            var viewName = _paymentService.PaymentWithCreditCard();
            return Content(viewName); // View("SuccessView");
        }
    }
}