using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.BLL.Services;
using Shop.BLL.Services.Infrastructure;
using Shop.Common.ViewModels;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    public class PaypalController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaypalController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Failure()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PaymentWithCreditCard()
        {
            var viewName = _paymentService.CreatePaymentWithCreditCard();
            return Content(viewName); // View("SuccessView");
        }

        [HttpPost]
        public ActionResult PaymentWithPaypalDemo()
        {
            var content = _paymentService.PaymentWithPaypalDemo(this.Request, this.Session);

            if (content.StartsWith("http"))
            {
                return Redirect(content);
            }
                
            return Content(content);
        }

        [HttpPost]
        public ActionResult PaymentWithPaypal(CartViewModel cart)
        {
            if (cart == null)
            {
                return RedirectToAction("Failure");
            }
            var status = _paymentService.PaymentWithPaypal(this.Request, this.Session, cart);

            if (status.StartsWith("http"))
            {
                return Redirect(status);
            }
            if(status == "Success")
            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failure");
            }
        }
    }
}