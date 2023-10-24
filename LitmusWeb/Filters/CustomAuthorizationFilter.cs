using DataAccess.Repositories;
using System.Web;
using System.Web.Mvc;
namespace LitmusWeb.Filters
{
    public class CustomAuthorizationFilter : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;
        public CustomAuthorizationFilter(params string[] roles)
        {
            this.allowedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            MasterUserRepository userRepository = new MasterUserRepository();
            bool authorize = false;
            if (httpContext.Session["UserCode"] == null)
            {
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var url = urlHelper.Action("Login", "Home");
                httpContext.Response.Clear();
                httpContext.Response.Redirect(url);
                httpContext.Response.End();
                return authorize;
            }
            var UserCode = httpContext.Session["UserCode"].ToString();
            if (!string.IsNullOrEmpty(UserCode))
            {
                authorize = userRepository.AuthorizeUserRole(UserCode, allowedRoles);
            }
            //return base.AuthorizeCore(httpContext);
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    {"controller","Error" },
                    {"action","Forbidden" }
                });
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}