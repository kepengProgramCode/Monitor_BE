namespace Monitor_BE.Entity
{
    public class LoginEntity
    {
        private string? username;
        private string? password;
        private bool rember;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Rember { get => rember; set => rember = value; }
    }
}
