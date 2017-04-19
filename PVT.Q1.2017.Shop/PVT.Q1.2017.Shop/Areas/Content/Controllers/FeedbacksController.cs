namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using App_Start;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.Infrastructure.Enums;
    using Shop.Controllers;

    [ShopAuthorize(UserRoles.Buyer, UserRoles.Customer)]
    public class FeedbacksController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            this._feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult Details(int? trackid)
        {
            if (trackid == null)
            {
                return this.RedirectToAction("List", "Track", new { area = "Content" });
            }

            var userDataId = GetUserDataId();
            if (userDataId != null)
            {
                ViewBag.CurrentFeedback = _feedbackService.GetTrackFeedback(trackid.Value, userDataId.Value);
            }

            var feedbacks = _feedbackService.GetTrackFeedbacks(trackid.Value);
            return View(feedbacks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include= "TrackId,Mark,Comments")]FeedbackViewModel feedback)
        {
            if (feedback == null)
            {
                return this.RedirectToAction("List", "Track", new { area = "Content" });
            }

            var userDataId = GetUserDataId();
            if (userDataId != null)
            {
                feedback.UserDataId = userDataId.Value;
                _feedbackService.AddOrUpdateFeedback(feedback);
            }

            return RedirectToAction("Details", "Feedbacks", new { area = "Content", trackId = feedback.TrackId });
        }
    }
}