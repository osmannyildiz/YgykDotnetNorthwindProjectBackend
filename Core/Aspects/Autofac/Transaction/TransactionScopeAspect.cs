using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Aspects/Autofac/Transaction/TransactionScopeAspect.cs
    public class TransactionScopeAspect : MethodInterception {
        public override void Intercept(IInvocation invocation) {
            using (TransactionScope transactionScope = new TransactionScope()) {
                try {
                    invocation.Proceed();
                    transactionScope.Complete();
                } catch (System.Exception e) {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
