using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService<T> where T: class
{
    CalculateRebateResult result { get; set; }
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
