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

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult MoreService()
        {
            return View();
        }
    }
}