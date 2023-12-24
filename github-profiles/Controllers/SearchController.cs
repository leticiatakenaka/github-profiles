using Microsoft.AspNetCore.Mvc;

namespace github_profiles.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
