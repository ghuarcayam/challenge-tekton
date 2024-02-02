using Autofac;
using Tekton.Module.Challenge.Application.Contract;
using Tekton.Module.Challenge.Infraestructure;

namespace Challenge.API.Modules.Challenge
{
    public class ChallengeAutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChallengeModule>().As<IChallengeModule>().InstancePerLifetimeScope();
        }
    }
}
