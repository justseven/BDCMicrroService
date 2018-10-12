using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using BDCMicrroService.Service.Contract;

namespace BDCMicrroService.Service
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
