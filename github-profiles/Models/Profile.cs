namespace github_profiles.Models
{
    public class Profile
    {
        public string login { get; set; }
        public string name { get; set; }
        public string avatar_url { get; set; }
        public string followers { get; set; }
        public int public_repos { get; set; }
        public string bio { get; set; }
    }
}
