using Autofac;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application;
using Tekton.Module.Challenge.Infraestructure.Configuration.Caching;
using Tekton.Module.Challenge.Infraestructure.Configuration.DataAccess;
using Tekton.Module.Challenge.Infraestructure.Configuration.ExternalService;
using Tekton.Module.Challenge.Infraestructure.Configuration.Logging;
using Tekton.Module.Challenge.Infraestructure.Configuration.Mediation;
using Tekton.Module.Challenge.Infraestructure.Configuration.Processing;

namespace Tekton.Module.Challenge.Infraestructure.Configuration
{
    public static class ChallengeStartup
    {
        public static void Start(string urlBase, ILogger logger, IExecutionContextAccessor executionContextAccessor) 
        {
            ConfigureCompositionRoot(urlBase, logger, executionContextAccessor);
        }
        private static void ConfigureCompositionRoot(string urlBase, ILogger logger, IExecutionContextAccessor executionContextAccessor) 
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            containerBuilder.RegisterModule(new MediatRModule());

            containerBuilder.RegisterModule(new ProcessingModule());

            containerBuilder.RegisterModule(new DataAccessModule());

            containerBuilder.RegisterModule(new CachingModule());

            containerBuilder.RegisterModule(new ExternalServiceModule(urlBase));

            containerBuilder.RegisterInstance(executionContextAccessor);

            var container = containerBuilder.Build();

            ChallengeCompositionRoot.SetContainer(container);
        }
    }
}
