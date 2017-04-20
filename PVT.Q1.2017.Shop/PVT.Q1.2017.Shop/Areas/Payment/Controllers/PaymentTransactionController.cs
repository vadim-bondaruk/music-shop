namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using System.Web.Mvc;
    using App_Start;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;
    using PVT.Q1._2017.Shop.Controllers;

    [ShopAuthorize]
    public class PaymentTransactionController : BaseController
    {
        public PaymentTransactionController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
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
    }
}