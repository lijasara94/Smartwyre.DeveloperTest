using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateDataStoreTests
    {
        [Fact]
        public void GetRebate_ShouldReturnRebate()
        {
            //Arrange
            var rebateDataStore = new RebateDataStore();
            var rebateIdentifier = "testrebate";

            //Act
            Rebate rebate = rebateDataStore.GetRebate(rebateIdentifier);

            //Assert
            Assert.NotNull(rebate);
        }
    }
}
