namespace PVT.Q1._2017.Shop.Areas.Administration
{
    using System.Web.Mvc;

    /// <summary>
    /// The administration area.
    /// </summary>
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// Gets the area name.
        /// </summary>
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}