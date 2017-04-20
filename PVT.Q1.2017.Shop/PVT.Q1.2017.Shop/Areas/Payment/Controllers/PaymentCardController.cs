
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PVT.Q1._2017.Shop.Controllers;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using App_Start;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;
    
    [ShopAuthorize]
    public class PaymentCardController : BaseController
    {
        public PaymentCardController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
        }

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