//
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
//
using Microsoft.Extensions.DependencyInjection;
using System;
//

namespace AutoFac.Infrastructure.CoreIoc {


    /// <summary>
    /// asp.net core 的AutoFac容器
    /// </summary>
    public static class CoreContainer {

        /// <summary>
        /// 容器实例
        /// </summary>
        public static IContainer Instance;


        public static IServiceProvider Init(IServiceCollection services, Func<ContainerBuilder, ContainerBuilder> func = null) {
            //新建容器构建起，用于注册组件和服务
            var builder = new ContainerBuilder();
            //将Core自带DI容器内的服务迁移到Autofac容器
            builder.Populate(services);

            //自定义注册组件
            MyBuild(builder);
            func?.Invoke(builder);
            Instance = builder.Build();

            return new AutofacServiceProvider(Instance);
        }

        public static void MyBuild(this ContainerBuilder builder) {
            var assemblies = Helpers.ReflectionHelper.GetAllAssembliesCoreWeb("Camefor");

            //注册仓储 %% Service
            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc => cc.Name.EndsWith("Repository") |
                           cc.Name.EndsWith("Services")) // | cc.Name.EndsWith("IRepository") | cc.Name.EndsWith("IServices")
                .PublicOnly()
                .Where(cc => cc.IsClass)
                .AsImplementedInterfaces().EnableInterfaceInterceptors();


            //注册泛型仓储
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //builder.RegisterGeneric(typeof(Camefor.Services.BaseServices<>)).As(typeof(Camefor.IServices.IBaseServices<>));

            //builder.RegisterType<Camefor.Repository.sugar.DbContext>()
            //.InstancePerLifetimeScope();

            /*
          //注册Controller
          Assembly[] controllerAssemblies = assemblies.Where(x => x.FullName.Contains(".CoreApi")).ToArray();
          builder.RegisterAssemblyTypes(controllerAssemblies)
              .Where(cc => cc.Name.EndsWith("Controller"))
              .AsSelf();
              */

        }
    }
}
