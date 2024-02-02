using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tekton.Module.Challenge.Application.Contract;
using Tekton.Module.Challenge.Application.Products.CreateProduct;
using Tekton.Module.Challenge.Application.Products.GetProduct;
using Tekton.Module.Challenge.Application.Products.UpdateProduct;

namespace Challenge.API.Modules.Challenge
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IChallengeModule challengeModule;
        public ProductController(IChallengeModule challengeModule) 
        {
            this.challengeModule = challengeModule;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestProduct request) 
        {
            var result = await challengeModule.ExecuteCommandAsync(new CreateProductCommand(request.Name, request.Stock, request.Description, request.Price));
            return this.Ok(result);
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> Update(Guid productId, [FromBody] RequestUpdateProduct request)
        {
            var result = await challengeModule.ExecuteCommandAsync(new UpdateProductCommand(productId, request.Name, request.Stock, request.Description, request.Price, request.Status));
            if (result == null || !result.Success) 
            {
                return NotFound(new { success = false });
            }
            return this.Ok(result);
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> Get(Guid productId)
        {
            var result = await challengeModule.ExecuteQueryAsync(new GetProductQuery(productId));
            if (result.Data == null)
                return NotFound(new { success = false });
            return this.Ok(result);
        }
    }
}
