
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public  class ProductDataStoreTests
    {
        [Fact]
        public void GetProduct_ShouldReturnProduct()
        {
            //Arrange
            var productDataStore = new ProductDataStore();
            var productIdentifier = "testproduct";

            //Act
            Product product = productDataStore.GetProduct(productIdentifier);

            //Assert
            Assert.NotNull(product);
        }
    }
}
