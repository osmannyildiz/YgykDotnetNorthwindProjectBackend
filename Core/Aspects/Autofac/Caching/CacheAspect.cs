using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Aspects/Autofac/Caching/CacheAspect.cs
    public class CacheAspect : MethodInterception {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) {
            _duration = duration;
            _cacheManager = ServiceHelper.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation) {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.Exists(key)) {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
