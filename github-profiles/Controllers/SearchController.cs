using github_profiles.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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
                ViewData["name"] = profile.name;
                ViewData["avatarUrl"] = profile.avatar_url;
                ViewData["followers"] = profile.followers;
                ViewData["repos"] = profile.public_repos;
                ViewData["bio"] = profile.bio;

                await GetTopRepos(username);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewData["message"] = "User not found";
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetTopRepos(string username)
        {
            using HttpClient httpClient = new();
            string apiUrl = $"https://api.github.com/users/{username}/repos?sort=stars&per_page=4";

            httpClient.DefaultRequestHeaders.Add("User-Agent", "leticiatakenaka");

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var repos = JsonConvert.DeserializeObject<List<Repository>>(content);
                ViewData["topFourRepos"] = repos;
            }

            return View("Index");
        }
    }
}
