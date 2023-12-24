using Microsoft.AspNetCore.Mvc;

namespace github_profiles.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Results(string username)
        {
            using HttpClient httpClient = new();
            string apiUrl = $"https://api.github.com/users/{username}";

            httpClient.DefaultRequestHeaders.Add("User-Agent", "leticiatakenaka");

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                ViewData["profile"] = content;
            }
            return View("Index");
        }
    }
}
