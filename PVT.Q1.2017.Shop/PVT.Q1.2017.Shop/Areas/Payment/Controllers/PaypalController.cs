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

        /// <summary>
        /// стартовая страница для демо-методов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод реакции на успешный статус оплаты
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public RedirectToRouteResult Success()
        {
            this.Session.Add("IsAccepted", true);
            // return RedirectToAction("AcceptPayment", "Cart", new { Area = "Payment" });
            return RedirectToAction("AcceptTransaction", "PaymentTransaction", new { Area = "Payment" });
        }

        /// <summary>
        /// Метод реакции на неуспешный статус оплаты
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult Failure()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ContentResult PaymentWithCreditCard()
        {
            var viewName = _paymentService.CreatePaymentWithCreditCard();
            return Content(viewName); // View("SuccessView");
        }

        /// <summary>
        /// Демо реализация платежей PayPal
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Метод вызывает диалог оплаты для пользователя на стороне PayPal
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
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
                this.Session.Add("IsAccepted", true);
                return RedirectToAction("Success", "Paypal", new { Area = "Payment" });
            }
            else
            {
                return RedirectToAction("Failure", "Paypal", new { Area = "Payment" });
            }
        }

        /// <summary>
        /// Метод обработки статуса платежа PayPal
        /// </summary>
        /// <returns></returns>
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