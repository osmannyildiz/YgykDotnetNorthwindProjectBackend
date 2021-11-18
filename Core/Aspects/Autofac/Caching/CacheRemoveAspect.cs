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
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern) {
            _pattern = pattern;
            _cacheManager = ServiceHelper.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
