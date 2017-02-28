namespace PVT.Q1._2017.Shop.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;

    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Redirect to local
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        protected virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }   

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Add errors to the current <see cref="ModelStateDictionary"/>
        /// </summary>
        /// <param name="errors"></param>
        protected virtual void AddErrors(IEnumerable<string> errors)
        {
            foreach (string errorMessage in errors)
            {
                this.ModelState.AddModelError(string.Empty, errorMessage);
            }
        }

        /// <summary>
        /// Render partial view to a string
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string RenderRazorPartialViewToString(string viewName, object model)
        {
            this.ViewData.Model = model;

            using (StringWriter stringWriter = new StringWriter())
            {
                ViewEngineResult partialView = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(this.ControllerContext, partialView.View, this.ViewData, this.TempData, stringWriter);
                partialView.View.Render(viewContext, stringWriter);
                partialView.ViewEngine.ReleaseView(this.ControllerContext, partialView.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Render view to a string
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string RenderRazorViewToString(string viewName, string masterName, object model)
        {
            this.ViewData.Model = model;

            using (StringWriter stringWriter = new StringWriter())
            {
                ViewEngineResult view = ViewEngines.Engines.FindView(this.ControllerContext, viewName, masterName);
                ViewContext viewContext = new ViewContext(this.ControllerContext, view.View, this.ViewData, this.TempData, stringWriter);
                view.View.Render(viewContext, stringWriter);
                view.ViewEngine.ReleaseView(this.ControllerContext, view.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}