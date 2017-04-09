using IPGMMS.Abstract;
using IPGMMS.DAL;
using IPGMMS.DAL.Repositories;
using IPGMMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Binding for the dbContexts
            kernel.Bind<IPGMMS_Context>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();

            // Binding for repositories
            kernel.Bind<IAccountRepository>().To<EFAccountRepository>().InRequestScope();
            kernel.Bind<IMemberRepository>().To<EFMemberRepository>().InRequestScope();
            kernel.Bind<IContactRepository>().To<EFContactRepository>().InRequestScope();
        }
    }
}