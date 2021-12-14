namespace DS.TaxCalculator.Services.Tests.TaxCalculatorServices.ImaginariaTaxCalculatorServiceTests
{
    using DS.TaxCalculator.Services.TaxCalculatorServices;
    using NUnit.Framework;

    public class ImaginariaTaxCalculatorServiceGetNetSalaryTests
    {

        [TestCase(980, 980)]
        [TestCase(1000, 1000)]
        [TestCase(999, 999)]
        [TestCase(3400, 2860)]
        [TestCase(3000, 2500)]
        [TestCase(3127.58, 2614.82)]
        public void GivenGetNetSalaryIsCalledThenNetSalaryIsCorrectlyCalculated(decimal grossSalary, decimal expectedNetSalary)
        {
            var imaginariaTaxCalculatorService = new ImaginariaTaxCalculatorService();

            var netSalary = imaginariaTaxCalculatorService.GetNetSalary(grossSalary);
            Assert.AreEqual(expectedNetSalary, netSalary);
        }
    }
}