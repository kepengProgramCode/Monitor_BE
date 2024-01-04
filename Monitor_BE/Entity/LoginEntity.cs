namespace Monitor_BE.Entity
{
    public class LoginEntity
    {
        private string? username;
        private string? password;
        private bool rember;

        public string? Username { get => username; set => username = value; }
        public string? Password { get => password; set => password = value; }
        public bool Rember { get => rember; set => rember = value; }
    }


    public class LoginResponse
    {
        private string? token;
        private string[]? auth;
        private int? role;

        public string? Token { get => token; set => token = value; }
        public string[]? Auth { get => auth; set => auth = value; }
        public int? Role { get => role; set => role = value; }
    }

}
