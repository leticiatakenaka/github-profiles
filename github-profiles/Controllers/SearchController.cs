using github_profiles.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

                var profile = JsonConvert.DeserializeObject<Profile>(content);
                ViewData["login"] = profile.login;
                ViewData["avatarUrl"] = profile.avatar_url;
                ViewData["followers"] = profile.followers;
                ViewData["repos"] = profile.public_repos;
            }

            return View("Index");
        }
    }
}
