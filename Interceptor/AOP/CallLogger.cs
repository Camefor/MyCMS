using Castle.DynamicProxy;
using System.IO;
using System.Linq;

namespace Interceptor.AOP {
    /// <summary>
    /// 拦截器
    /// <br/>
    /// 参考:
    /// https://juejin.im/post/5df1b750f265da33b12e9e81
    /// </summary>
    public class CallLogger : IInterceptor {
        TextWriter _output;
        public CallLogger(TextWriter output) {
            _output = output;
        }
        public void Intercept(IInvocation invocation) {
            _output.WriteLine("Calling method {0} with parameters {1}...",
                invocation.Method.Name, string.Join(", ",
                invocation.Arguments.Select(a => (a ?? "").ToString())).ToArray());
            invocation.Proceed();

            _output.WriteLine("Done: result was {0}.", invocation.ReturnValue);

        }
    }
}
