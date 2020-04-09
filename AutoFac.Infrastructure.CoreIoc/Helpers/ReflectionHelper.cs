using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace AutoFac.Infrastructure.CoreIoc.Helpers {

    /// <summary>
    ///  获取Asp.Net Core项目所有程序集
    /// </summary>
    /// <returns></returns>
    public static class ReflectionHelper {

        /// <summary>
        /// 反射获取程序集
        /// </summary>
        /// <param name="Prefix">程序集前缀名</param>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesCoreWeb(string Prefix) {
            var list = new List<Assembly>();
            DependencyContext dependencyContext = DependencyContext.Default;
            IEnumerable<CompilationLibrary> libs = dependencyContext.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package" && lib.Name.StartsWith(Prefix));
            foreach (var lib in libs) {
                Assembly assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                list.Add(assembly);
            }
            return list.ToArray();
        }
    }
}
