
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Challenge.API.Modules.Challenge;
using Serilog.Formatting.Compact;
using Serilog;
using Challenge.API.Configuration.ExecutionContext;
using Tekton.Module.Challenge.Infraestructure.Configuration;
using Tekton.Module.Challenge.Application;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Challenge.API.Configuration.Extensions;
using Challenge.API.Configuration;

namespace Challenge.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Serilog.ILogger _logger = null;
            Serilog.ILogger _loggerForApi = null;

            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            var host = builder.Host;
            var configuration = builder.Configuration;

            host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new ChallengeAutofacModule());
                
            });

            // Add services to the container.
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddEndpointsApiExplorer();
            

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tekton Challenge API",
                    Version = "v1",
                    Description = "Tekton Challenge API - Giancarlo Huarcaya giancarlohm@gmail.com"
                });
            });

            var app = builder.Build();

            app.UseChallengeExceptionHandler();

            ConfigureLogger(app, ref _logger, ref _loggerForApi);

            //services.AddSingleton(_loggerForApi);

            app.UseMiddleware<CorrelationMiddleware>();
            app.UseMiddleware<ResponseTimeMeterMiddleware>(_loggerForApi);

            var container = app.Services.GetAutofacRoot();

            

            InitializeModules(configuration, container, _logger);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        static void InitializeModules(IConfiguration configuration, ILifetimeScope container, Serilog.ILogger _logger)
        {
            string connectionstringkey = "ConnectionStrings:challenge";
            string urlBaseKey = "urlServices:serviceExternal";

            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();

            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            var connectionstring = configuration[connectionstringkey];

            var urlBase = configuration[urlBaseKey];

            ChallengeStartup.Start(urlBase, _logger, executionContextAccessor);
            
        }
        public static void ConfigureLogger(WebApplication app, ref Serilog.ILogger _logger, ref Serilog.ILogger _loggerForApi)
        {
            var con = new LoggerConfiguration()
                .Enrich.FromLogContext().WriteTo.File(new CompactJsonFormatter(), "logs/logs");

            _logger = con.CreateLogger();


            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }
    }
   
}
