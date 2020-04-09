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

        private const string ApiName = "Camefor的博客项目";

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers().AddControllersAsServices();
            //services.AddSqlsugarSetup();

            //以下两种方式是 在 不 使用 AutoFac作为 容器时
            //即在 使用自带DI时候在这里注册



            //1th
            //services.AddMyServices();


            //======2th======
            //自定义批量注册
            //Assembly[] assemblies = AutoFac.Infrastructure.CoreIoc.Helpers.ReflectionHelper.GetAllAssembliesCoreWeb("Camefor");
            //services.AddAutoMapper(assemblies);
            ////注册repository
            //Assembly repositoryAssemblies = assemblies.FirstOrDefault(x => x.FullName.Contains(".Repository"));
            //services.AddAssemblyServices(repositoryAssemblies);

            ////services.AddScoped(new SqlSugarClient());
            ////注册service  
            //Assembly serviceAssemblies = assemblies.FirstOrDefault(x => x.FullName.Contains(".Services"));
            //services.AddAssemblyServices(serviceAssemblies);




            #region Swagger
            services.AddSwaggerGen(c =>
            {

                #region 多版本
                //遍历出全部的版本，做文档信息展示
                //typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                //{
                //    c.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo {
                //        // {ApiName} 定义成全局变量，方便修改
                //        Version = version,
                //        Title = $"{ApiName} 接口文档",
                //        Description = $"{ApiName} HTTP API " + version,
                //        TermsOfService = null,
                //        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Autho.JWT", Email = "Autho.JWT@xxx.com", Url = new Uri("http://www.camefor.top/") }
                //    });
                //    // 按相对路径排序，作者：Alby
                //    c.OrderActionsBy(o => o.RelativePath);
                //});

                #endregion 多版本

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "API文档",
                    TermsOfService = new Uri("http://www.camefor.top/"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Blog.Core", Email = "xuehaq@gmail.com", Url = new Uri("http://www.camefor.top/") }
                });

                #region  用于生成API文档注释
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Camefor.xml");
                c.IncludeXmlComments(xmlPath, true);

                //导入model.xml到swagger
                var xmlModelPath = Path.Combine(basePath, "Camefor.Model.xml");
                c.IncludeXmlComments(xmlModelPath);

                #endregion  用于生成API文档注释

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                //发行人
                //https://blog.csdn.net/weixin_33008495/article/details/102811347
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
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
                #endregion Token绑定到ConfigureServices

            });
            #endregion

            #region 认证


            //读取配置文件
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
                    IssuerSigningKey = signingKey,//参数配置在下边
                    ValidateIssuer = true,
                    ValidIssuer = audienceConfig["Issuer"],//发行人
                    ValidateAudience = true,
                    ValidAudience = audienceConfig["Audience"],//订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };
            });
            #endregion 认证

            #region 授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//单独角色
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//或的关系
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//且的关系
            });


            #endregion 授权



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
                //c.RoutePrefix = "swagger";//路径配置
                c.RoutePrefix = "";//路径配置，设置为空。表示直接访问该文件
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
        /// v1 版本
        /// </summary>
        v1 = 1,
        /// <summary>
        /// v2 版本
        /// </summary>
        v2 = 2,
    }
}
