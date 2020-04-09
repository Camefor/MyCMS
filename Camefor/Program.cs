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

                    //��һ�� ʹ���Դ�DI
                    //webBuilder.UseStartup<Startup>();

                    //�ڶ��֣����AutoFac��Ϊ��������
                    //�޸�program�࣬��AutoFac hook���ܵ�������StartupWithAutoFac��ָ��Ϊע����ڣ�
                    webBuilder.HookAutoFacIntoPipeline().UseStartup<StartupWithAutoFac>();

                    //������:���AutoFac�ӹ�����ע��
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
