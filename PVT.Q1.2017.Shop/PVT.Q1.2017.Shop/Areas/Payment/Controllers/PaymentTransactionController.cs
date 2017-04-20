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
    using Helpers;

    public class PaymentTransactionController : BaseController
    {

        private IPaymentService _paymentService;

        public PaymentTransactionController(IPaymentService payService)
        {
            _paymentService = payService;
        }         

        /// <summary>
        /// Action for 
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]
        [Authorize]
        public ActionResult Index(int id = 1)
        {
            if (CurrentUser != null && CurrentUser.Identity.IsAuthenticated)
            {
                int countPerPage = 10;
                this.TempData["CurrentPage"] = id;
                var transactions = _paymentService.GetDataPerPage(CurrentUser.Id, id, countPerPage);
                return this.View(PaymentMapper.GetPaymentTransactionsToEdit(transactions));

                //var transactions = _paymentService.GetTransactionsByUserId(CurrentUser.Id);
                //var transactionsViewModels = PaymentMapper.GetPaymentTransactionViewModels(transactions);
                //return View(transactionsViewModels);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int? transactionID)
        {
            //TODO: реализовать просмотр деталей заказа
            if(transactionID==null)
            {
                return HttpNotFound();
            }
            
            return View();
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