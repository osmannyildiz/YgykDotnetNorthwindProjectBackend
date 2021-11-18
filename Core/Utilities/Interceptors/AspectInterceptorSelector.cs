using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Utilities/Interceptors/AspectInterceptorSelector.cs
    public class AspectInterceptorSelector : IInterceptorSelector {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) {
            var attributes = new List<MethodInterceptionBaseAttribute>();
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            attributes.AddRange(classAttributes);
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            attributes.AddRange(methodAttributes);
            //attributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            attributes.Add(new PerformanceAspect(2));

            return attributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
