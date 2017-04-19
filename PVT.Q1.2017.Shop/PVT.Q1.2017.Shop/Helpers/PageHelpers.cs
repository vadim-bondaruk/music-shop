namespace PVT.Q1._2017.Shop.Helpers
{
    using System;
    using System.Text;
    using System.Web.Mvc;
    using global::Shop.Infrastructure.Models;

    /// <summary>
    /// Class for page displaying
    /// </summary>
    public static class PageHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageInfo"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks<T>(this HtmlHelper helper, PagedResult<T> pageInfo, Func<int, string> url)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", url(1));
            tag.InnerHtml = "First";
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());

            for (int i = pageInfo.CurrentPage - 2; i <= pageInfo.CurrentPage + 2; i++)
            {
                if (i > 0 && i <= pageInfo.PagesCount)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", url(i));
                    tag.InnerHtml = i.ToString();

                    if (i == pageInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn btn-primary");
                    }
                    else
                    {
                        tag.AddCssClass("btn btn-default");
                    }

                    result.Append(tag.ToString());
                }
            }

            tag = new TagBuilder("a");
            tag.MergeAttribute("href", url(pageInfo.PagesCount));
            tag.InnerHtml = "Last";
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());

            return new MvcHtmlString(result.ToString());
        }
    }
}