using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Autofac.Builder;
using Autofac.Core;
using Autofac;
using System.Reflection;
using Microsoft.AspNet.Identity;
using AdminGujaratiSamaj.Models;
using AdminGujaratiSamaj.DAL;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using AdminGujaratiSamaj.Models.UserManagement;

namespace AdminGujaratiSamaj
{
    public static class Bootstraper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AGSDBContext())))
              .As<UserManager<ApplicationUser>>().InstancePerRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}