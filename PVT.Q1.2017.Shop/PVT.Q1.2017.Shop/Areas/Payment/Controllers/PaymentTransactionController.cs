namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PVT.Q1._2017.Shop.Controllers;

    public class PaymentTransactionController : BaseController
    {
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
    }
}