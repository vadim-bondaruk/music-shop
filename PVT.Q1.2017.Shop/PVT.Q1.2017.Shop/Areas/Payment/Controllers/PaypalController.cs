using Shop.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Payment/Paypal
        public ActionResult Index()
        {
            return View();
        }

        // GET: Payment/Paypal
        public ActionResult PaymentWithCreditCard()
        {

            //var payServ = new PaymentService();
            return View("SuccessView");
        }
    }
}