//微软包
//三方包
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace AutoFac.Infrastructure.CoreIoc.Extensions {
    /// <summary>
    /// AutoFac扩展
    /// </summary>
    public static class AutoFacExtension {
        /// <summary>
        /// 在asp.net core程序启动时，将AutoFac加入管道
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder HookAutoFacIntoPipeline(this IWebHostBuilder builder) {
            builder.ConfigureServices(services => services.AddAutofac());
            return builder;
        }
    }
}
