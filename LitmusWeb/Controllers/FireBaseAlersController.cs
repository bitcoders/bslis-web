using LitmusWeb.Models.CustomModels;
using System.Web.Mvc;

namespace LitmusWeb.Controllers
{
    public class FireBaseAlersController : Controller
    {
        // GET: FireBaseAlers
        public ActionResult Index()
        {
            FirebaseCustomModel firebaseCustomModel = new FirebaseCustomModel();
            return View();
        }

        [HttpPost]
        public ActionResult IndexPost()
        {
            return View();
        }
    }
}