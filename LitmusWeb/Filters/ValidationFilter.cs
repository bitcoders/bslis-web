using DataAccess.Repositories;
using System.Web;
using System.Web.Mvc;


namespace LitmusWeb.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        private readonly string operationType;
        ValidationRepository validationRepository = new ValidationRepository();

        public ValidationFilter(string operations)
        {
            this.operationType = operations;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserCode"] == null)
            {
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var url = urlHelper.Action("Login", "Home");
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Redirect(url);
                filterContext.HttpContext.Response.End();
            }
            string userCode, crushingSeason;
            if (filterContext.HttpContext.Session["UserCode"] != null)
            {
                userCode = filterContext.HttpContext.Session["UserCode"].ToString();
                crushingSeason = filterContext.HttpContext.Session["CrushingSeason"].ToString();

                switch (operationType.ToLower())
                {
                    case "create":
                        if (validationRepository.EntryAllowedForSeason(userCode, crushingSeason) == false)
                        {
                            string message = "You are not allowed to create a new record for season code " + crushingSeason;
                            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                            var url = urlHelper.Action("Forbidden", "Error", new { errorMessage = message });
                            filterContext.HttpContext.Response.Clear();
                            filterContext.HttpContext.Response.Redirect(url);
                            filterContext.HttpContext.Response.End();
                        }
                        break;
                    case "view":
                        if (validationRepository.ViewAllowedForSeason(userCode, crushingSeason) == false)
                        {
                            string message = "You are not allowed to View Data/Report for season code " + crushingSeason;
                            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                            var url = urlHelper.Action("Forbidden", "Error", new { errorMessage = message });
                            filterContext.HttpContext.Response.Clear();
                            filterContext.HttpContext.Response.Redirect(url);
                            filterContext.HttpContext.Response.End();
                        }
                        break;
                    case "update":
                        if (validationRepository.UpdationAllowedForSeason(userCode, crushingSeason) == false)
                        {
                            string message = "You are not allowed to update/modify for season code " + crushingSeason;
                            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                            var url = urlHelper.Action("Forbidden", "Error", new { errorMessage = message });
                            filterContext.HttpContext.Response.Clear();
                            filterContext.HttpContext.Response.Redirect(url);
                            filterContext.HttpContext.Response.End();
                        }
                        break;
                    case "edit":
                        if (validationRepository.UpdationAllowedForSeason(userCode, crushingSeason) == false)
                        {
                            string message = "You are not allowed to update/modify for season code " + crushingSeason;
                            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                            var url = urlHelper.Action("Forbidden", "Error", new { errorMessage = message });
                            filterContext.HttpContext.Response.Clear();
                            filterContext.HttpContext.Response.Redirect(url);
                            filterContext.HttpContext.Response.End();
                        }
                        break;
                    default:

                        break;
                }
            }


        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}