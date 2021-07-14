using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Utilities/Interceptors/AspectInterceptorSelector.cs
    public class AspectInterceptorSelector : IInterceptorSelector {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
