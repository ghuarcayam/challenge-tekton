using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Products;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Infraestructure.IntegrationService
{
    internal class StatusProductService : IStatusProductService
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger logger;
        private readonly HttpClientChallenge httpClientChallenge;
        private readonly string urlBase;
        const string KEYCACHE = "StatusProduct";
        public StatusProductService(string urlBase, IMemoryCache memoryCache, ILogger logger, HttpClientChallenge httpClientChallenge) 
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
            this.httpClientChallenge = httpClientChallenge;
            this.urlBase = urlBase;
        }
        public async Task<string> GetStatusName(int status)
        {
            var statuses = await GetStatuses();
            string name = string.Empty;
            statuses.TryGetValue(status, out name);
            return name;

        }

        private async Task<IDictionary<int, string>> GetStatuses() 
        {
            var statuses = memoryCache.Get(KEYCACHE) as IDictionary<int, string>;

            if (statuses == null)
            {
                logger.Information("Get the state names from the service");
                statuses = await getFromService();
                memoryCache.Set(KEYCACHE, statuses, TimeSpan.FromMinutes(5));
            }
            return statuses;
        }

        private async Task<IDictionary<int, string>> getFromService() 
        {
            var items  = await this.httpClientChallenge.SendGet<IEnumerable<ItemRequestStatusProduct>>($"{urlBase}/productStatus");
            return  items.ToDictionary(x => x.Id, y => y.StatusName);
        }

        private class ItemRequestStatusProduct 
        {
            public int Id { get; set; }
            public string StatusName { get; set; }
        }
    }
}
