namespace MyTeam.Common.Requests.Auth
{
    public class RegisterRequest
    {
        public string id { get; set; }
        public string username { get; set; }
        public string fullName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int role { get; set; }
        public string roleLabel { get; set; }
    }
}
