namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PVT.Q1._2017.Shop.Controllers;
    using global::Shop.Common.ViewModels;
    using global::Shop.BLL.Services.Infrastructure;

    public class PaymentTransactionController : BaseController
    {

        private IPaymentService _paymentService;

        public PaymentTransactionController(IPaymentService payService)
        {
            _paymentService = payService;
        }         

        // GET: Payment/PaymentTransaction
        [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]
        [Authorize]
        public ActionResult Index()
        {
            if (CurrentUser != null && CurrentUser.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult AcceptTransaction()
        {
            var accept = false;
            if (Session["IsAccepted"] != null)
            {
                accept = (bool)Session["IsAccepted"];
                AcceptTransaction(accept);
            }
            return RedirectToAction("AcceptPayment", "Cart", new { Area = "Payment" });

        }

        [HttpPost]
        [Authorize]
        public ActionResult AcceptTransaction(bool accept)
        {
            CartViewModel cart = (CartViewModel)TempData["cart"];
            if(accept)
            {
                _paymentService.CreatePaymentTransaction(cart);
            }
            return RedirectToAction("AcceptPayment", "Cart", new { Area = "Payment" });
        }
    }
}