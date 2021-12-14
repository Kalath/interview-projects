namespace DS.TaxCalculator.Services.TaxCalculatorServices
{
    public interface IImaginariaTaxCalculatorService
    {
        /// <summary>
        /// Calculates Net Salary using decimals and returns rounded to 2 decimal symbols salary
        /// </summary>
        /// <param name="personGrossSalary"></param>
        /// <returns></returns>
        decimal GetNetSalary(decimal personGrossSalary); // Using decimals as they have better precision when working with money
    }
}