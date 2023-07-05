using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Runner
{
    public class DIContainer
    {
        public static ServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection().
                                         AddTransient<IRebateDataStore, RebateDataStore>()
                                        .AddTransient<IProductDataStore, ProductDataStore>()
                                        .AddTransient<IRebateService<FixedRateRebateService>, FixedRateRebateService>()
                                        .AddTransient<IRebateService<FixedCashRebateService>, FixedCashRebateService>()
                                        .AddTransient<IRebateService<AmountPerUomRebateService>, AmountPerUomRebateService>()
                                        ;
            return serviceCollection.BuildServiceProvider();
        }
    }
}
