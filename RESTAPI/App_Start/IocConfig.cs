using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using RESTAPI.Data;
using RESTAPI.Data.Concrete;
using RESTAPI.Data.Data;


namespace RESTAPI
{
    public class IocConfig
    {
        public static void RegisterConfigurations()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DatabaseEntities>().InstancePerRequest();

            builder.RegisterType<ArtistRepository>().As<IArtistRepository>();
            builder.RegisterType<VideoRepository>().As<IVideoRepository>();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}