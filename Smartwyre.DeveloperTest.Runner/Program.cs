using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = DIContainer.Configure();
        bool shouldContinue = true;
        while (shouldContinue)
        {
            Console.WriteLine("To claculate rebate please enter \n" +
                " \n" +
                          "1. RebateIdentifier (string) \n");


            var rebateIdentifier = Console.ReadLine();
            Console.WriteLine("\n 2. ProductIdentifier (string)\n");
            var productIdentifier = Console.ReadLine();

            Console.WriteLine("\n 3. Volume (decimal)\n");
            var volume = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("\n");
            Console.WriteLine("Since database is not developed getting the incentive type as user input for now . \n" +
                "Select the incentive type\n" +
                " \n" +
                          "1. FixedRateRebate \n" +
                          "2. AmountPerUom \n" +
                          "3. FixedCashAmount \n");

            var option = Convert.ToInt32(Console.ReadLine());
            CalculateRebateRequest calculateRebateRequest = new CalculateRebateRequest();   
            calculateRebateRequest.RebateIdentifier = rebateIdentifier;
            calculateRebateRequest.ProductIdentifier = productIdentifier;
            calculateRebateRequest.Volume = volume;

            var rebateDataStore = serviceProvider.GetService<IRebateDataStore>();
            Rebate rebate = rebateDataStore.GetRebate(rebateIdentifier);

            // gets the incentive tpe from user for now because the database is not developed yet
            rebate.Incentive = (IncentiveType)(option-1);

            if (rebate.Incentive == IncentiveType.FixedRateRebate)
            {
                var rebateservice = serviceProvider.GetService<IRebateService<FixedRateRebateService>>();
                Console.WriteLine("CalculateRebateResult.Success = " + rebateservice.Calculate(calculateRebateRequest).Success + "\n");
            }
            else if (rebate.Incentive == IncentiveType.FixedCashAmount)
            {
                var rebateservice = serviceProvider.GetService<IRebateService<FixedCashRebateService>>();
                Console.WriteLine("CalculateRebateResult.Success = " + rebateservice.Calculate(calculateRebateRequest).Success + "\n");
            }
            else if (rebate.Incentive == IncentiveType.AmountPerUom)
            {
                var rebateservice = serviceProvider.GetService<IRebateService<AmountPerUomRebateService>>();
                Console.WriteLine("CalculateRebateResult.Success = " + rebateservice.Calculate(calculateRebateRequest).Success +"\n");
            }

            Console.WriteLine("Would you like to continue? Say 'Yes' Or 'No' \n");
            var choice = Console.ReadLine();
            Console.WriteLine();
            if (choice.ToLower()=="yes")
                shouldContinue = true;
            else
                shouldContinue = false;

           
        }
    }
}
