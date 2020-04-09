using AutoFac.Infrastructure.CoreIoc.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Camefor {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
               //https://stackoverflow.com/questions/59680121/unable-to-cast-object-of-type-servicecollection-to-type-autofac-containerbuilde
               .UseServiceProviderFactory(new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    //第一种 使用自带DI
                    //webBuilder.UseStartup<Startup>();

                    //第二种：添加AutoFac作为辅助容器
                    //修改program类，将AutoFac hook进管道，并将StartupWithAutoFac类指定为注册入口：
                    webBuilder.HookAutoFacIntoPipeline().UseStartup<StartupWithAutoFac>();

                    //第三种:添加AutoFac接管依赖注入
                    //webBuilder.UseStartup<StartupOnlyAutoFac>();

                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.AddEventLog();
                });
    }
}
