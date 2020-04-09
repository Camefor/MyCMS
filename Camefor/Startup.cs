using Camefor.Repository.sugar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Camefor {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string ApiName = "Camefor�Ĳ�����Ŀ";

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers().AddControllersAsServices();
            //services.AddSqlsugarSetup();

            //�������ַ�ʽ�� �� �� ʹ�� AutoFac��Ϊ ����ʱ
            //���� ʹ���Դ�DIʱ��������ע��



            //1th
            //services.AddMyServices();


            //======2th======
            //�Զ�������ע��
            //Assembly[] assemblies = AutoFac.Infrastructure.CoreIoc.Helpers.ReflectionHelper.GetAllAssembliesCoreWeb("Camefor");
            //services.AddAutoMapper(assemblies);
            ////ע��repository
            //Assembly repositoryAssemblies = assemblies.FirstOrDefault(x => x.FullName.Contains(".Repository"));
            //services.AddAssemblyServices(repositoryAssemblies);

            ////services.AddScoped(new SqlSugarClient());
            ////ע��service  
            //Assembly serviceAssemblies = assemblies.FirstOrDefault(x => x.FullName.Contains(".Services"));
            //services.AddAssemblyServices(serviceAssemblies);




            #region Swagger
            services.AddSwaggerGen(c =>
            {

                #region ��汾
                //������ȫ���İ汾�����ĵ���Ϣչʾ
                //typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                //{
                //    c.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo {
                //        // {ApiName} �����ȫ�ֱ����������޸�
                //        Version = version,
                //        Title = $"{ApiName} �ӿ��ĵ�",
                //        Description = $"{ApiName} HTTP API " + version,
                //        TermsOfService = null,
                //        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Autho.JWT", Email = "Autho.JWT@xxx.com", Url = new Uri("http://www.camefor.top/") }
                //    });
                //    // �����·���������ߣ�Alby
                //    c.OrderActionsBy(o => o.RelativePath);
                //});

                #endregion ��汾

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "API�ĵ�",
                    TermsOfService = new Uri("http://www.camefor.top/"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Blog.Core", Email = "xuehaq@gmail.com", Url = new Uri("http://www.camefor.top/") }
                });

                #region  ��������API�ĵ�ע��
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Camefor.xml");
                c.IncludeXmlComments(xmlPath, true);

                //����model.xml��swagger
                var xmlModelPath = Path.Combine(basePath, "Camefor.Model.xml");
                c.IncludeXmlComments(xmlModelPath);

                #endregion  ��������API�ĵ�ע��

                #region Token�󶨵�ConfigureServices
                //���header��֤��Ϣ
                //c.OperationFilter<SwaggerHeader>();

                //������
                //https://blog.csdn.net/weixin_33008495/article/details/102811347
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
                #endregion Token�󶨵�ConfigureServices

            });
            #endregion

            #region ��֤


            //��ȡ�����ļ�
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



            }).AddJwtBearer(o =>
            {
                o.Events = new JwtBearerEvents() {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    }
                };

                o.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,//�����������±�
                    ValidateIssuer = true,
                    ValidIssuer = audienceConfig["Issuer"],//������
                    ValidateAudience = true,
                    ValidAudience = audienceConfig["Audience"],//������
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };
            });
            #endregion ��֤

            #region ��Ȩ
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//������ɫ
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//��Ĺ�ϵ
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//�ҵĹ�ϵ
            });


            #endregion ��Ȩ



            BaseDBConfig.ConnectionString = Configuration.GetConnectionString("CameforDbContext");
            //BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //c.RoutePrefix = "swagger";//·������
                c.RoutePrefix = "";//·�����ã�����Ϊ�ա���ʾֱ�ӷ��ʸ��ļ�
            });
            #endregion Swagger

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    public enum ApiVersions {
        /// <summary>
        /// v1 �汾
        /// </summary>
        v1 = 1,
        /// <summary>
        /// v2 �汾
        /// </summary>
        v2 = 2,
    }
}
