using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    public class PaymentCardController : Controller
    {
        // GET: Payment/PaymentCard
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { Area = "User" });
            }
            return View();
        }
    }
}