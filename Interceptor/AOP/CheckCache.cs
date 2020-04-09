using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interceptor.AOP {
    /// <summary>
    /// 检查方法是否需要缓存
    /// </summary>
    public class CheckCache : IInterceptor {

        //private readonly ICache;
        public CheckCache() {

        }
        public void Intercept(IInvocation invocation) {
            var cacheable = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(CacheableAttribute), true).Length > 0;
            if (cacheable) {
                //执行方法前先检查缓存，如果缓存中有数据直接返回
            }

            invocation.Proceed();
            if (cacheable) {
                //执行方法后将方法返回值写入缓存
            }


        }
    }
}
