using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac {
    public class AutofacBusinessModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();

            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();

            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();

            builder.RegisterType<JwtTokenHelper>().As<ITokenHelper>();

            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Business/DependencyResolvers/Autofac/AutofacBusinessModule.cs
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
