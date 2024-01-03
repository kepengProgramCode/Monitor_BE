using Microsoft.IdentityModel.Tokens;
using Monitor_BE.Entity;
using NewLife.Remoting;
using NewLife;
using NewLife.Security;
using NewLife.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using static Dm.net.buffer.ByteArrayBuffer;

namespace Monitor_BE.Common.Token
{
    public class TokenAuth
    {
        public static JwtAuthConfig jwtConfig { get; }
        static TokenAuth()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            //jwt配置文件
            jwtConfig = new JwtAuthConfig(config);
        }
        /// <summary>
        /// 颁发JWT字符串,自定义格式
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(tb_token tokenModel)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.u_id.ToString()),//用户Id
                new Claim("Role", tokenModel.role),//身份
                new Claim("Project", tokenModel.project),//身份
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64)
            };

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            //秘钥
            var jwtConfig = new JwtAuthConfig(config);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.TokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //过期时间
            double exp = 0;
            switch (tokenModel.tokenType)
            {
                case "Web":
                    exp = jwtConfig.WebExp;
                    break;
                case "App":
                    exp = jwtConfig.AppExp;
                    break;
                case "MiniProgram":
                    exp = jwtConfig.MiniProgramExp;
                    break;
                case "Other":
                    exp = jwtConfig.OtherExp;
                    break;
            }
            var jwt = new JwtSecurityToken(
                issuer: "RayPI",
                claims: claims, //声明集合
                expires: dateTime.AddHours(exp),
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }


        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static tb_token SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object(); ;
            object project = new object();
            try
            {
                jwtToken.Payload.TryGetValue("Role", out role);
                jwtToken.Payload.TryGetValue("Project", out project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new tb_token
            {
                //Uid = jwtToken.Id,
                role = role.ToString(),
                project = project.ToString()
            };
            return tm;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public static TokenModel IssueToken(string name)
        {
            // 颁发令牌
            var ss = jwtConfig.TokenSecret.Split(':');
            var jwt = new JwtBuilder
            {
                Issuer = Assembly.GetEntryAssembly()?.GetName().Name,
                Subject = name,
                Id = Rand.NextString(8),
                Expire = DateTime.Now.AddSeconds(jwtConfig.WebExp),

                Algorithm = ss[0],
                Secret = ss[1],
            };

            return new TokenModel
            {
                AccessToken = jwt.Encode(null),
                TokenType = jwt.Type ?? "JWT",
                ExpireIn = jwtConfig.WebExp.ToInt(),
                RefreshToken = jwt.Encode(null),
            };
        }

        public static bool DecodeToken(string token, string tokenSecret)
        {
            if (token.IsNullOrEmpty()) throw new ArgumentNullException(nameof(token));
            //if (token.IsNullOrEmpty()) throw new ApiException(402, $"节点未登录[ip={UserHost}]");

            // 解码令牌
            var ss = tokenSecret.Split(':');
            var jwt = new JwtBuilder
            {
                Algorithm = ss[0],
                Secret = ss[1],
            };

            return jwt.TryDecode(token, out var message);
        }

        public static TokenModel? ValidAndIssueToken(string deviceCode, string token)
        {
            if (token.IsNullOrEmpty()) return null;
            //var set = Setting.Current;

            // 令牌有效期检查，10分钟内过期者，重新颁发令牌
            var ss = jwtConfig.TokenSecret.Split(':');
            var jwt = new JwtBuilder
            {
                Algorithm = ss[0],
                Secret = ss[1],
            };
            var rs = jwt.TryDecode(token, out var message);
            return !rs || jwt == null ? null : DateTime.Now.AddMinutes(10) > jwt.Expire ? IssueToken(deviceCode) : null;
        }
    }
}
