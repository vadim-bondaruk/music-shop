﻿namespace PVT.Q1._2017.Shop.Areas.News.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// </summary>
    public class MusicController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Article(string partialName)
        {
            this.ViewBag.Title = "Новости музыки";
            return this.View(partialName);
        }
    }
}