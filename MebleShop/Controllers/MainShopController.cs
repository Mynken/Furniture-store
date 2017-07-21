using System.Web.Mvc;

namespace MebleShop.Controllers
{
    public class MainShopController : Controller
    {
        // GET: MainShop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult MoreService()
        {
            return View();
        }
    }
}