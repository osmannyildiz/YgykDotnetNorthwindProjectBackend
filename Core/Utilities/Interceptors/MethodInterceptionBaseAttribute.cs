using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Utilities/Interceptors/MethodInterceptionBaseAttribute.cs
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation) { }
    }
}
