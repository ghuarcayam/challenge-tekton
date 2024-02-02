using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.UnitTests
{
    public class TestsRed
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStatusProductMustBeActiveFail()
        {
            var product = new Product(
                            "Product Test", 12, "Test", 100);

            Assert.AreNotEqual(product.Status, StatusProduct.Inactive.GetHashCode());

        }

        [Test]
        public void TestFinalPriceInvalid()
        {
            var product = new Product(
                            "Product Test", 12, "Test", 100);

            Assert.AreNotEqual(product.ApplyDiscount(12), 85);

        }
    }
}