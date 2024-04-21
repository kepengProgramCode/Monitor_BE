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
        //���ÿ���̨��־ newLife
        XTrace.UseConsole();
        var builder = WebApplication.CreateBuilder(args);

        #region ���Autofac����ע��
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

        // ���ýӿ���Ӧѹ��
        builder.Services.AddResponseCompression();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        #region ���Swagger����
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "������token,��ʽΪ Bearer xxxxxxxx��ע���м�����пո�",
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


        #region �����֤ �����ļ����Է���appsetting.json��
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
        //    //��Կ
        //    JwtAuthConfigModel jwtConfig = new JwtAuthConfigModel(config);
        //    s.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidIssuer = "RayPI",
        //        ValidAudience = "wr",
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecurityKey)),

        //        /***********************************TokenValidationParameters�Ĳ���Ĭ��ֵ***********************************/
        //        RequireSignedTokens = true,
        //        // SaveSigninToken = false,
        //        // ValidateActor = false,
        //        // ������������������Ϊfalse�����Բ���֤Issuer��Audience�����ǲ�������������
        //        ValidateAudience = false,
        //        ValidateIssuer = true,
        //        ValidateIssuerSigningKey = true,
        //        // �Ƿ�Ҫ��Token��Claims�б������ Expires
        //        RequireExpirationTime = true,
        //        // ����ķ�����ʱ��ƫ����
        //        // ClockSkew = TimeSpan.FromSeconds(300),
        //        // �Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
        //        ValidateLifetime = true
        //    };
        //});
        #endregion


        #region �����Ȩ
        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("RequireClient", policy => policy.RequireRole("Client").Build());
        //    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin").Build());
        //    options.AddPolicy("RequireAdminOrClient", policy => policy.RequireRole("Admin,Client").Build());
        //});
        #endregion


        #region �������� ��Ҫ����ȷ�� UseCors �������� UseRouting �� UseEndpoints ֮ǰ��
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

        // �����֤�м��
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseStaticFiles(); // ���þ�̬�ļ�����

        app.Run();
    }
}
