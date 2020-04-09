using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Camefor.Interceptor {
    /// <summary>
    /// AspectCore Project 是适用于Asp.Net Core 平台的轻量级 Aop(Aspect-oriented programming) 解决方案，
    /// 它更好的遵循Asp.Net Core的模块化开发理念，使用AspectCore可以更容易构建低耦合、易扩展的Web应用程序
    /// </summary>
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute {
        public override async Task Invoke(AspectContext context, AspectDelegate next) {
            try {
                Console.WriteLine("Before service call");
                await next(context);
            } catch (Exception) {
                Console.WriteLine("Service threw an exception!");
                throw;
            } finally {
                Console.WriteLine("After service call");
            }
        }
    }
}
