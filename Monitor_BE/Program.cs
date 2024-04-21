using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Monitor_BE.Common.Token;
using NewLife.Log;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        //启用控制台日志 newLife
        XTrace.UseConsole();
        var builder = WebApplication.CreateBuilder(args);

        #region 添加Autofac依赖注入
        // Add services to the container.
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(options =>
        {
            try
            {
                var assembly = Assembly.Load("Monitor_BE");
                options.RegisterAssemblyTypes(assembly).Where(o => o.Name.EndsWith("Service")).AsSelf();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.InnerException);
            }
        });
        #endregion

        // 启用接口响应压缩
        builder.Services.AddResponseCompression();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        #region 添加Swagger界面
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        #endregion


        #region 添加认证 配置文件可以放在appsetting.json中
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        //builder.Services.AddAuthentication(x =>
        //{
        //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).
        //AddJwtBearer(s =>
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        //    var config = builder.Build();
        //    //秘钥
        //    JwtAuthConfigModel jwtConfig = new JwtAuthConfigModel(config);
        //    s.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidIssuer = "RayPI",
        //        ValidAudience = "wr",
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecurityKey)),

        //        /***********************************TokenValidationParameters的参数默认值***********************************/
        //        RequireSignedTokens = true,
        //        // SaveSigninToken = false,
        //        // ValidateActor = false,
        //        // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
        //        ValidateAudience = false,
        //        ValidateIssuer = true,
        //        ValidateIssuerSigningKey = true,
        //        // 是否要求Token的Claims中必须包含 Expires
        //        RequireExpirationTime = true,
        //        // 允许的服务器时间偏移量
        //        // ClockSkew = TimeSpan.FromSeconds(300),
        //        // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
        //        ValidateLifetime = true
        //    };
        //});
        #endregion


        #region 添加授权
        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("RequireClient", policy => policy.RequireRole("Client").Build());
        //    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin").Build());
        //    options.AddPolicy("RequireAdminOrClient", policy => policy.RequireRole("Admin,Client").Build());
        //});
        #endregion


        #region 跨域问题 重要的是确保 UseCors 被调用在 UseRouting 和 UseEndpoints 之前。
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("my", builder =>
            {
                builder.WithOrigins("*").
                AllowAnyHeader().
                AllowAnyMethod();
            });

            //options.AddPolicy("Limit", policy =>
            //{
            //    policy
            //    .WithOrigins("localhost:7266")
            //    .WithMethods("get", "post", "put", "delete")
            //    //.WithHeaders("Authorization");
            //    .AllowAnyHeader();
            //});
        });

        #endregion

        var app = builder.Build();

        app.UseCors("my");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseMiddleware<JwtAuthorizationFilter>();

        //app.UseHttpsRedirection();

        // 添加认证中间件
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseStaticFiles(); // 启用静态文件服务

        app.Run();
    }
}
