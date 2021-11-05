using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Ioc {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Utilities/IoC/ServiceTool.cs
    public static class ServiceHelper {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
