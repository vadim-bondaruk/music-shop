// --------------------------------------------------------------------------------------------------------------------
// <copyright file="T4MVC.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the MVC type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#pragma warning disable 1591, 3008, 3009, 0108, 0114

#region T4MVC

#region

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

using T4MVC;

#endregion

/// <summary>
/// </summary>
[GeneratedCode("T4MVC", "2.0")]
[DebuggerNonUserCode]
public static class MVC
{
    /// <summary>
    /// </summary>
    public static SharedController Shared = new SharedController();
}

namespace T4MVC
{
}

#pragma warning disable 0436

namespace T4MVC
{
    /// <summary>
    /// </summary>
    [GeneratedCode("T4MVC", "2.0")]
    [DebuggerNonUserCode]
    public class Dummy
    {
        /// <summary>
        /// </summary>
        public static Dummy Instance = new Dummy();

        /// <summary>
        ///     Prevents a default instance of the <see cref="Dummy" /> class from
        ///     being created.
        /// </summary>
        private Dummy()
        {
        }
    }
}

#pragma warning restore 0436

namespace Links
{
    #region

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// </summary>
    [GeneratedCode("T4MVC", "2.0")]
    [DebuggerNonUserCode]
    public static class Scripts
    {
        /// <summary>
        /// </summary>
        public const string UrlPath = "~/Scripts";

        /// <summary>
        /// </summary>
        public static readonly string bootstrap_js = T4MVCHelpers.IsProduction()
                                                     && T4Extensions.FileExists(UrlPath + "/bootstrap.min.js")
                                                         ? Url("bootstrap.min.js")
                                                         : Url("bootstrap.js");

        /// <summary>
        /// </summary>
        public static readonly string bootstrap_min_js = Url("bootstrap.min.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_1_10_2_intellisense_js = T4MVCHelpers.IsProduction()
                                                                      && T4Extensions.FileExists(
                                                                          UrlPath + "/jquery-1.10.2.intellisense.min.js")
                                                                          ? Url("jquery-1.10.2.intellisense.min.js")
                                                                          : Url("jquery-1.10.2.intellisense.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_1_10_2_js = T4MVCHelpers.IsProduction()
                                                         && T4Extensions.FileExists(UrlPath + "/jquery-1.10.2.min.js")
                                                             ? Url("jquery-1.10.2.min.js")
                                                             : Url("jquery-1.10.2.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_1_10_2_min_js = Url("jquery-1.10.2.min.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_1_10_2_min_map = Url("jquery-1.10.2.min.map");

        /// <summary>
        /// </summary>
        public static readonly string jquery_2_1_3_intellisense_js = T4MVCHelpers.IsProduction()
                                                                     && T4Extensions.FileExists(
                                                                         UrlPath + "/jquery-2.1.3.intellisense.min.js")
                                                                         ? Url("jquery-2.1.3.intellisense.min.js")
                                                                         : Url("jquery-2.1.3.intellisense.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_unobtrusive_ajax_js = T4MVCHelpers.IsProduction()
                                                                   && T4Extensions.FileExists(
                                                                       UrlPath + "/jquery.unobtrusive-ajax.min.js")
                                                                       ? Url("jquery.unobtrusive-ajax.min.js")
                                                                       : Url("jquery.unobtrusive-ajax.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_unobtrusive_ajax_min_js = Url("jquery.unobtrusive-ajax.min.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_validate_vsdoc_js = T4MVCHelpers.IsProduction()
                                                                 && T4Extensions.FileExists(
                                                                     UrlPath + "/jquery.validate-vsdoc.min.js")
                                                                     ? Url("jquery.validate-vsdoc.min.js")
                                                                     : Url("jquery.validate-vsdoc.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction()
                                                           && T4Extensions.FileExists(
                                                               UrlPath + "/jquery.validate.min.js")
                                                               ? Url("jquery.validate.min.js")
                                                               : Url("jquery.validate.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_validate_min_js = Url("jquery.validate.min.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_validate_unobtrusive_js = T4MVCHelpers.IsProduction()
                                                                       && T4Extensions.FileExists(
                                                                           UrlPath
                                                                           + "/jquery.validate.unobtrusive.min.js")
                                                                           ? Url("jquery.validate.unobtrusive.min.js")
                                                                           : Url("jquery.validate.unobtrusive.js");

        /// <summary>
        /// </summary>
        public static readonly string jquery_validate_unobtrusive_min_js = Url("jquery.validate.unobtrusive.min.js");

        /// <summary>
        /// </summary>
        public static readonly string modernizr_2_6_2_js = T4MVCHelpers.IsProduction()
                                                           && T4Extensions.FileExists(
                                                               UrlPath + "/modernizr-2.6.2.min.js")
                                                               ? Url("modernizr-2.6.2.min.js")
                                                               : Url("modernizr-2.6.2.js");

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public static string Url()
        {
            return T4MVCHelpers.ProcessVirtualPath(UrlPath);
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>
        /// </returns>
        public static string Url(string fileName)
        {
            return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName);
        }
    }

    /// <summary>
    /// </summary>
    [GeneratedCode("T4MVC", "2.0")]
    [DebuggerNonUserCode]
    public static class Content
    {
        /// <summary>
        /// </summary>
        public const string UrlPath = "~/Content";

        /// <summary>
        /// </summary>
        public static readonly string bootstrap_css = T4MVCHelpers.IsProduction()
                                                      && T4Extensions.FileExists(UrlPath + "/bootstrap.min.css")
                                                          ? Url("bootstrap.min.css")
                                                          : Url("bootstrap.css");

        /// <summary>
        /// </summary>
        public static readonly string bootstrap_min_css = Url("bootstrap.min.css");

        /// <summary>
        /// </summary>
        public static readonly string Site_css = T4MVCHelpers.IsProduction()
                                                 && T4Extensions.FileExists(UrlPath + "/Site.min.css")
                                                     ? Url("Site.min.css")
                                                     : Url("Site.css");

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public static string Url()
        {
            return T4MVCHelpers.ProcessVirtualPath(UrlPath);
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>
        /// </returns>
        public static string Url(string fileName)
        {
            return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName);
        }
    }

    /// <summary>
    /// </summary>
    [GeneratedCode("T4MVC", "2.0")]
    [DebuggerNonUserCode]
    public static class Bundles
    {
        /// <summary>
        /// </summary>
        public static class Content
        {
            /// <summary>
            /// </summary>
            public static class Assets
            {
                /// <summary>
                /// </summary>
                public const string bootstrap_css = "~/Content/bootstrap.css";

                /// <summary>
                /// </summary>
                public const string bootstrap_min_css = "~/Content/bootstrap.min.css";

                /// <summary>
                /// </summary>
                public const string Site_css = "~/Content/Site.css";
            }
        }

        /// <summary>
        /// </summary>
        public static class Scripts
        {
            /// <summary>
            /// </summary>
            public static class Assets
            {
                /// <summary>
                /// </summary>
                public const string bootstrap_js = "~/Scripts/bootstrap.js";

                /// <summary>
                /// </summary>
                public const string bootstrap_min_js = "~/Scripts/bootstrap.min.js";

                /// <summary>
                /// </summary>
                public const string jquery_1_10_2_intellisense_js = "~/Scripts/jquery-1.10.2.intellisense.js";

                /// <summary>
                /// </summary>
                public const string jquery_1_10_2_js = "~/Scripts/jquery-1.10.2.js";

                /// <summary>
                /// </summary>
                public const string jquery_1_10_2_min_js = "~/Scripts/jquery-1.10.2.min.js";

                /// <summary>
                /// </summary>
                public const string jquery_2_1_3_intellisense_js = "~/Scripts/jquery-2.1.3.intellisense.js";

                /// <summary>
                /// </summary>
                public const string jquery_unobtrusive_ajax_js = "~/Scripts/jquery.unobtrusive-ajax.js";

                /// <summary>
                /// </summary>
                public const string jquery_unobtrusive_ajax_min_js = "~/Scripts/jquery.unobtrusive-ajax.min.js";

                /// <summary>
                /// </summary>
                public const string jquery_validate_js = "~/Scripts/jquery.validate.js";

                /// <summary>
                /// </summary>
                public const string jquery_validate_min_js = "~/Scripts/jquery.validate.min.js";

                /// <summary>
                /// </summary>
                public const string jquery_validate_unobtrusive_js = "~/Scripts/jquery.validate.unobtrusive.js";

                /// <summary>
                /// </summary>
                public const string jquery_validate_unobtrusive_min_js = "~/Scripts/jquery.validate.unobtrusive.min.js";

                /// <summary>
                /// </summary>
                public const string modernizr_2_6_2_js = "~/Scripts/modernizr-2.6.2.js";
            }
        }
    }
}

/// <summary>
/// </summary>
[GeneratedCode("T4MVC", "2.0")]
[DebuggerNonUserCode]
internal static class T4MVCHelpers
{
    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    /// <summary>
    /// </summary>
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    /// <summary>
    /// </summary>
    public static Func<string, string> TimestampString = T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    public static bool IsProduction()
    {
        return HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled;
    }

    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    // return "http://localhost" + path + "?foo=bar";
    /// <summary>
    /// </summary>
    /// <param name="virtualPath">The <see langword="virtual" /> path.</param>
    /// <returns>
    /// </returns>
    private static string ProcessVirtualPathDefault(string virtualPath)
    {
        // The path that comes in starts with ~/ and must first be made absolute
        var path = VirtualPathUtility.ToAbsolute(virtualPath);

        // Add your own modifications here before returning the path
        return path;
    }
}

#endregion T4MVC

#pragma warning restore 1591, 3008, 3009, 0108, 0114