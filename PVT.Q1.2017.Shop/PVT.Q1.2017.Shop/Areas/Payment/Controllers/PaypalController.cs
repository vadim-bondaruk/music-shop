using System.Web.Mvc;
using Shop.BLL.Services.Infrastructure;
using Shop.Common.ViewModels;
using PVT.Q1._2017.Shop.Controllers;

namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using App_Start;
    using global::Shop.DAL.Infrastruture;
    
    [ShopAuthorize]
    public class PaypalController : BaseController
    {
        public PaypalController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
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
            var paymentService = ServiceFactory.GetPaymentService();
            var viewName = paymentService.CreatePaymentWithCreditCard();
            return Content(viewName); // View("SuccessView");
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentWithPaypalDemo()
        {
            var paymentService = ServiceFactory.GetPaymentService();
            var status = paymentService.PaymentWithPaypalDemo(Request, Session);

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

            var paymentService = ServiceFactory.GetPaymentService();
            var status = paymentService.PaymentWithPaypal(Request, Session, cart);

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
            var paymentService = ServiceFactory.GetPaymentService();
            var status = paymentService.PaymentWithPaypal(Request, Session);

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