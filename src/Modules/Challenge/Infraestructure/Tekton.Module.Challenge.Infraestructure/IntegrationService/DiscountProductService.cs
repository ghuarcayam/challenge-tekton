using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Products;

namespace Tekton.Module.Challenge.Infraestructure.IntegrationService
{
    internal class DiscountProductService : IDiscountProductService
    {
        private readonly HttpClientChallenge httpClientChallenge;
        private readonly string urlBase;
        public DiscountProductService(HttpClientChallenge httpClientChallenge, string urlBase) 
        {
            this.httpClientChallenge = httpClientChallenge;
            this.urlBase = urlBase;
        }
        public async Task<int?> Get(Guid productId)
        {
            try
            {
                var discount = await this.httpClientChallenge.SendGet<ItemResponseDiscount>($"{urlBase}/discount/{productId}");
                if (discount != null)
                {
                    return discount.Discount;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                else
                    throw;
            }
           
        }
    }

    internal class ItemResponseDiscount 
    {
        public int Discount { get; set; }
        public string Id { get; set; }
    }
}
