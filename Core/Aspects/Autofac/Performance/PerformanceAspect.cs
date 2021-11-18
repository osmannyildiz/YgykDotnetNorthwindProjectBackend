using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Aspects.Autofac.Performance {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Aspects/Autofac/Performance/PerformanceAspect.cs
    public class PerformanceAspect : MethodInterception {
        private int _thresholdSeconds;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int thresholdSeconds) {
            _thresholdSeconds = thresholdSeconds;
            _stopwatch = ServiceHelper.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation) {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation) {
            if (_stopwatch.Elapsed.TotalSeconds > _thresholdSeconds) {
                Debug.WriteLine($"Performance Warning: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} took {_stopwatch.Elapsed.TotalSeconds} seconds to execute.");
            }
            _stopwatch.Reset();
        }
    }
}
