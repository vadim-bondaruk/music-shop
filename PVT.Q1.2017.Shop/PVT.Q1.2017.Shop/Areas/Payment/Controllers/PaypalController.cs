using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.BLL.Services;
using Shop.BLL.Services.Infrastructure;
using Shop.Common.ViewModels;
using PVT.Q1._2017.Shop.Controllers;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    public class PaypalController : BaseController
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

        [Authorize]
        public ActionResult Success()
        {
            return View();
        }

        [Authorize]
        public ActionResult Failure()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentWithCreditCard()
        {
            var viewName = _paymentService.CreatePaymentWithCreditCard();
            return Content(viewName); // View("SuccessView");
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentWithPaypalDemo()
        {
            var status = _paymentService.PaymentWithPaypalDemo(this.Request, this.Session);

            if (status.StartsWith("http"))
            {
                return Redirect(status);
            }
            if (status == "Success")
            {
                return RedirectToAction("Success", "Paypal", new { Area = "Payment"});
            }
            else
            {
                return RedirectToAction("Failure", "Paypal", new { Area = "Payment" });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult PaymentWithPaypal(CartViewModel cart)
        {
            cart = (CartViewModel)TempData["cart"];

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
                return RedirectToAction("Success", "Paypal", new { Area = "Payment" });
            }
            else
            {
                return RedirectToAction("Failure", "Paypal", new { Area = "Payment" });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentWithPaypal()
        {
            var status = _paymentService.PaymentWithPaypal(this.Request, this.Session);

            if (status.StartsWith("http"))
            {
                return Redirect(status);
            }
            else
            {
                return RedirectToAction(status, "Paypal", new { Area = "Payment" });
            }
        }
    }
}