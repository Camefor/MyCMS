using System;

namespace Camefor.Services.Attribute {
    /// <summary>
    /// 自定义特性Cacheable
    /// 将这个注解标注在方法上，会对方法的返回结果进行缓存
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheableAttribute : System.Attribute {

    }
}
