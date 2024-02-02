using Tekton.Module.Challenge.Application.Products.CreateProduct;
using Tekton.Module.Challenge.Application.Products.GetProduct;
using Tekton.Module.Challenge.Application.Products.UpdateProduct;

namespace Tekton.Module.Challenge.IntegrationTests
{
    public class Tests: TestBase
    {
       

        [Test]
        public async Task Create_Product_Test()
        {
            var result = await ChallengeModule.ExecuteCommandAsync(new CreateProductCommand(
                ProductSampleData.Name, 
                ProductSampleData.Stock, 
                ProductSampleData.Description,
                ProductSampleData.Price));
            var resultProduct = await ChallengeModule.ExecuteQueryAsync(new GetProductQuery(result.Data));
            Assert.IsNotNull(resultProduct);
            Assert.True(resultProduct.Success);
            Assert.That(resultProduct.Data.Name, Is.EqualTo(ProductSampleData.Name));
            Assert.That(resultProduct.Data.Stock, Is.EqualTo(ProductSampleData.Stock));
            Assert.That(resultProduct.Data.Description, Is.EqualTo(ProductSampleData.Description));
            Assert.That(resultProduct.Data.Price, Is.EqualTo(ProductSampleData.Price));
        }

        [Test]
        public async Task Update_Product_Test()
        {
            var result = await ChallengeModule.ExecuteCommandAsync(new CreateProductCommand(
               ProductSampleData.Name,
               ProductSampleData.Stock,
               ProductSampleData.Description,
               ProductSampleData.Price));
            var resultProduct = await ChallengeModule.ExecuteQueryAsync(new GetProductQuery(result.Data));
            Assert.IsNotNull(resultProduct);
            Assert.True(resultProduct.Success);

            var resultUpdate = await ChallengeModule.ExecuteCommandAsync(new UpdateProductCommand(result.Data,
                ProductSampleData.Name,
                ProductSampleData.Stock+1,
                ProductSampleData.Description,
                ProductSampleData.Price*2,
                ProductSampleData.Status));

            var resultProductUpdate = await ChallengeModule.ExecuteQueryAsync(new GetProductQuery(result.Data));

            Assert.IsNotNull(resultProductUpdate);
            Assert.True(resultProductUpdate.Success);
            Assert.That(resultProductUpdate.Data.Name, Is.EqualTo(ProductSampleData.Name));
            Assert.That(resultProductUpdate.Data.Stock, Is.EqualTo(ProductSampleData.Stock+1));
            Assert.That(resultProductUpdate.Data.Description, Is.EqualTo(ProductSampleData.Description));
            Assert.That(resultProductUpdate.Data.Price, Is.EqualTo(ProductSampleData.Price*2));
            Assert.That(resultProductUpdate.Data.StatusName, Is.EqualTo(ProductSampleData.StatusName));
        }

        
    }
}