using Camefor.Services.Attribute;
using Camefor.Services.TEST;
using Castle.DynamicProxy;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Camefor.Services.Interceptor {
    /// <summary>
    /// 检查方法是否需要缓存
    /// </summary>
    public class CheckCache : IInterceptor {
        private readonly ICache _cache;
        public CheckCache(ICache cache) {
            this._cache = cache;
        }
        public void Intercept(IInvocation invocation) {
            var cacheable = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(CacheableAttribute),true).Length > 0;
            string key = "";
            if (cacheable) {
                //执行方法前先检查缓存，如果缓存中有数据直接返回

                key = invocation.Method.Name + string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
                var cacheValue = _cache.Get(key);
                if (cacheValue != null) {
                    if (invocation.Method.ReturnType.IsInstanceOfType(cacheValue)) {
                        invocation.ReturnValue = cacheValue;
                        return;
                    }
                }
            }
            invocation.Proceed();
            if (cacheable) {
                //执行方法后将方法返回值写入缓存
                _cache.Put(key, invocation.ReturnValue);
            }
        }
    }
}
