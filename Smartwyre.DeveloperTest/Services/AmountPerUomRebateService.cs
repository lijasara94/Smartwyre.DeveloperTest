using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class AmountPerUomRebateService : IRebateService<AmountPerUomRebateService>
    {
        private readonly IProductDataStore _productDataStore;
        private readonly IRebateDataStore _rebateDataStore;
        public AmountPerUomRebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore)
        {
            _productDataStore = productDataStore;
            _rebateDataStore = rebateDataStore;
        }

        public CalculateRebateResult result { get; set; } = new CalculateRebateResult();

        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            if(request == null) 
                throw new ArgumentNullException(nameof(request));

            Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            Product product = _productDataStore.GetProduct(request.ProductIdentifier);

            var rebateAmount = 0m;
            if (rebate == null)
            {
                result.Success = false;
            }
            else if (product == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                rebateAmount += rebate.Amount * request.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
