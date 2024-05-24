namespace Monitor_BE.Entity
{
    public class LoginEntity
    {
        private string username;
        private string password;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }


    public class LoginResponse
    {
        //private string? token;
        //private string[]? auth;
        //private int? role;

        public string? access_token { get; set; }
        //public string[]? Auth { get => auth; set => auth = value; }
        //public int? Role { get => role; set => role = value; }
    }

    public class LogoutResponse { }

    public class ResLists<T>
    {
        public List<T> list { get; set; }
        public int total { get => list.Count; }
        public int pageSize { get; set; }
        public int pageNum { get; set; } = 1;

    }

    public class GetrPar<T>
    {
        public int pageSize { get; set; }
        public int pageNum { get; set; }
        public T? dynamicParams { get; set; }
    }

    public class UploadRes
    {
        public string fileUrl { get; set; }
    }
}
