using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers {
    public class CoreModule : ICoreModule {
        public void Load(IServiceCollection services) {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddMemoryCache();

            services.AddSingleton<Stopwatch>();
        }
    }
}
