using ProjektHermods.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjektHermods.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index()
        {
            return View();
        }
    }
}