using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Products;
using Tekton.Module.Challenge.Infraestructure.IntegrationService;

namespace Tekton.Module.Challenge.Infraestructure.Configuration.ExternalService
{
    internal class ExternalServiceModule: Autofac.Module
    {
        private readonly string urlBase;
        public ExternalServiceModule(string urlBase) 
        {
            this.urlBase = urlBase;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IHttpClientFactory>(_ =>
            {
                var services = new ServiceCollection();
                services.AddHttpClient();
                var provider = services.BuildServiceProvider();
                return provider.GetRequiredService<IHttpClientFactory>();
            }).InstancePerLifetimeScope();

            builder.RegisterType<HttpClientChallenge>().InstancePerLifetimeScope();

            builder.RegisterType<DiscountProductService>()
               .AsImplementedInterfaces()
               .WithParameter("urlBase", urlBase)
               .InstancePerLifetimeScope();

            builder.RegisterType<StatusProductService>()
               .AsImplementedInterfaces()
               .WithParameter("urlBase", urlBase)
               .InstancePerLifetimeScope();
        }
    }
}
