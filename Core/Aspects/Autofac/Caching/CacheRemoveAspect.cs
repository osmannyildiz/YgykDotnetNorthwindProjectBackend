using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Caching {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Aspects/Autofac/Caching/CacheRemoveAspect.cs
    public class CacheRemoveAspect : MethodInterception {
        private string[] _patterns;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(params string[] patterns) {
            _patterns = patterns;
            _cacheManager = ServiceHelper.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) {
            foreach (var pattern in _patterns) {
                _cacheManager.RemoveByPattern(pattern);
            }
        }
    }
}
