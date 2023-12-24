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

                await GetTopRepos(username);
            }
            else
            {
                var dumbProfile = new Profile()
                {
                    login = "leticiatakenaka",
                    followers = "20",
                    avatar_url = "https://avatars.githubusercontent.com/u/99449687?v=4",
                    public_repos = 30,
                };

                ViewData["login"] = dumbProfile.login;
                ViewData["avatarUrl"] = dumbProfile.avatar_url;
                ViewData["followers"] = dumbProfile.followers;
                ViewData["repos"] = dumbProfile.public_repos;

                var dumbTopRepos = new List<Repository>()
                {
                    new()
                    {
                        name = "Introducao_POO_Controle_Estoque",
                        html_url = "https://github.com/leticiatakenaka/Introducao_POO_Controle_Estoque",
                    },
                    new()
                    {
                        name = "Introducao_POO_Exercicio_2",
                        html_url = "https://github.com/leticiatakenaka/Introducao_POO_Exercicio_2",
                        description = "Fazer um programa para ler nome e salário de dois funcionários. Depois, mostrar o salário médio dos funcionários"
                    }, new()
                    {
                        name = "Introducao_POO_Exercicio_1",
                        html_url = "https://github.com/leticiatakenaka/Introducao_POO_Exercicio_1",
                        description = "Fazer um programa para ler os dados de duas pessoas, depois mostrar o nome da pessoa mais velha."
                    }

            };
                ViewData["topFourRepos"] = dumbTopRepos;
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
