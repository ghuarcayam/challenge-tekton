

using Tekton.Module.Challenge.Domain;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.UnitTests
{
    public class TestGreen
    {
        [Test]
        public void TestNegativeNumbersNotallowedDiscount() 
        {
            var product = new Product(
                           "Product Test", 12, "Test", 100);
            Assert.Catch<BusinessRuleValidationException>(() => product.ApplyDiscount(-10));
        }

        [Test]
        public void TestPriceNegativeCatch()
        {
            Assert.Catch<BusinessRuleValidationException>(() => new Product(
                           "Product Test", -12, "Test", 100));
        }
    }
}
