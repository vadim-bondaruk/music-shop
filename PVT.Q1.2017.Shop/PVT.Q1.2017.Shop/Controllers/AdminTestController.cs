using PVT.Q1._2017.Shop.App_Start;
using Shop.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Controllers
{
    public class AdminTestController : BaseController
    {
        // GET: Admin
        [ShopAuthorize(UserRoles.User)]
        public ActionResult Index()
        {
            ViewBag.UserID = this.CurrentUser.Id;
            return View();
        }
    }
}