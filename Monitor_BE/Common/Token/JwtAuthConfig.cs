namespace Monitor_BE.Common.Token
{
    public class JwtAuthConfig
    {
        private readonly IConfigurationSection _configSection;

        public JwtAuthConfig(IConfiguration configuration)
        {
            _configSection = configuration.GetSection("JwtAuth");
        }
        public string Issuer => _configSection.GetValue("Issuer", "RayPI");
        public string Audience => _configSection.GetValue("Audience", "MyAudience");

        /// <summary>
        ///<summary>令牌密钥。用于生成JWT令牌的算法和密钥，如HS256:ABCD1234</summary>
        /// </summary>
        public string TokenSecret => _configSection.GetValue("TokenSecret", "HS256:ABCD1234");

        /// <summary>
        /// Web端过期时间,默认2小时
        /// </summary>
        public double WebExp => _configSection.GetValue<double>("WebExp", 3600);

        /// <summary>
        /// 移动端过期时间
        /// </summary>
        public double AppExp => _configSection.GetValue<double>("AppExp", 30);

        /// <summary>
        /// 小程序过期时间
        /// </summary>
        public double MiniProgramExp => _configSection.GetValue<double>("MiniProgramExp", 30);

        /// <summary>
        /// 其他端过期时间
        /// </summary>
        public double OtherExp => _configSection.GetValue<double>("OtherExp", 30);
    }
}
