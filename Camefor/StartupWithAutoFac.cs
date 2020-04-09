using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoFac.Infrastructure.CoreIoc;
using Camefor.Repository.sugar;
using Camefor.Services.Interceptor;
using Camefor.Services.TEST;
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

    /// <summary>
    /// AutoFac作为辅助注册#
    /// 这里其他地方与原startup都相同，
    /// 只是多了一个ConfigureContainer()方法，
    /// 在该方法内可以按照AutoFac的语法进行自由注册
    /// </summary>
    public class StartupWithAutoFac {

        public IConfiguration Configuration { get; }

        public StartupWithAutoFac(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            //services.AddSqlsugarSetup();

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
                    Contact = new OpenApiContact { Name = "Blog.Core", Email = "xuehaq@gmail.com", Url = new Uri("http://www.camefor.top/") }
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
        }

        /// <summary>
        /// 利用该方法可以使用AutoFac辅助注册，该方法在ConfigureServices()之后执行，所以当发生覆盖注册时，以后者为准。
        /// 不要再利用构建器去创建AutoFac容器了，系统已经接管了。
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder) {
            builder.MyBuild();
            //注册拦截器
            builder.Register(c => new CallLogger(Console.Out)); //注册拦截器。拦截器要注册到Autofac容器中。
            builder.RegisterType<CheckCache>();
            //注册
            //这个缓存类啥也不干，既不会插入值，也不会返回值，只是为了测试，实际情况中可能要实现MemeryCache或RedisCache等。 
            builder.RegisterType<NoOpCache>().As<ICache>().EnableInterfaceInterceptors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //c.RoutePrefix = "swagger";//路径配置
                c.RoutePrefix = "";//路径配置，设置为空。表示直接访问该文件
            });
            #endregion Swagger

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
