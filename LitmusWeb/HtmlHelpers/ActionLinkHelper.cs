using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LitmusWeb.HtmlHelpers
{
    public static class ActionLinkHelper
    {
        //public static MvcHtmlString ActionLinkCustom(this HtmlHelper htmlHelper, string linkText, string ActionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string,object> htmlAttributes, bool ShowActionLinkAsDisabled)
        //{
        //    TagBuilder tag = new TagBuilder("a");
        //    return htmlHelper.ActionLink(linkText, controllerName, routeValues,htmlAttributes);
        //}
        public static MvcHtmlString ActionLinkCustom(this HtmlHelper htmlHelper, string linkText, string ActionName, string controllerName, string cssClassName, bool ShowActionLinkAsDisabled)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.AddCssClass(cssClassName);
            return htmlHelper.ActionLink(linkText, ActionName, controllerName, cssClassName);
        }
    }
}