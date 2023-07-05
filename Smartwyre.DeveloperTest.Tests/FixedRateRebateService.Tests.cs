using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class FixedRateRebateServiceTests
    {
        [Fact]
        public void Calculate_ShouldReturn_CalculateRebateResult_Success()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 6, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 8, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 5 });

            FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 4 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.True(calculateRebateResult.Success);

        }

        [Fact]
        public void Calculate_Failed_For_NullRebate()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 6, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns((Types.Rebate)null);

             FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 4 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.False(calculateRebateResult.Success);
        }

        [Fact]
        public void Calculate_Failed_For_NullProduct()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns((Types.Product)null);

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 8, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 5 });

             FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 4 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.False(calculateRebateResult.Success);
        }

        [Fact]
        public void Calculate_Failed_For_Not_Supported_Incentive()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 6, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 8, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 5 });

             FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 4 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.False(calculateRebateResult.Success);

        }

        [Fact]
        public void Calculate_Failed_For_Zero_Rebate_Percentage()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 6, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 2, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 0 });

             FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 4 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.False(calculateRebateResult.Success);

        }
        [Fact]
        public void Calculate_Failed_For_Zero_Product_Price()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 0, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 2, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 0 });

            FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 4 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.False(calculateRebateResult.Success);

        }

        [Fact]
        public void Calculate_Failed_For_Zero_Request_Voulme()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 6, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 8, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 5 });

             FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproductidentifier", RebateIdentifier = "testrebateidentifier", Volume = 0 };

            // Act

            CalculateRebateResult calculateRebateResult = fixedRateRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.False(calculateRebateResult.Success);

        }

        [Fact]
        public void Calculate_Throws_Exception()
        {
            // Arrange
            Mock<IProductDataStore> mProductDataStore = new Mock<IProductDataStore>();
            mProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(new Types.Product() { Id = 1, Identifier = "prd1", Price = 6, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "test" });

            Mock<IRebateDataStore> mRebateDataStore = new Mock<IRebateDataStore>();
            mRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(new Types.Rebate() { Amount = 8, Identifier = "rbt1", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 5 });

             FixedRateRebateService fixedRateRebateService = new FixedRateRebateService(mProductDataStore.Object, mRebateDataStore.Object);

            CalculateRebateRequest request = null;

            // Act
            Action action = () => fixedRateRebateService.Calculate(request);

            // Assert
            var caughtException = Assert.Throws<ArgumentNullException>(action);


        }
    }
}
