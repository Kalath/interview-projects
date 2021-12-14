namespace DS.TaxCalculator.Services.TaxCalculatorServices
{
    public class ImaginariaTaxCalculatorService : IImaginariaTaxCalculatorService
    {
        // Given that taxation rules can be changed in time it's best to get them from some sort of DB.
        // Based on use-case DB can vary, for the example will be SQLite as it can be shipped alongside the product.
        // 2 tables:
        // Country (Id*, Country, CountryCurrencyCode)
        // ImaginariaTaxationRules (Id*, TaxationCountryId (FK), MinimalTaxTreshold, IncomeTaxPercent, SocialTaxPercent, MaximalSocialTreshold, FromDate, ToDate, DateChanged).
        // Create View to return ImaginariaTaxRules with Date input param.
        // Repository layer to access DB which returns DTO (no ORM framework).
        // Not going with the above solution as per requirements it's not clear whether client is in hurry or not. So the below is an easy upgradable MVP (Minimal Valuable Product).
        const decimal minimalTaxTreshold = 1000;

        /// <summary>
        /// Calculates Net Salary of residence of Imaginaria using decimals with round precision of 2
        /// </summary>
        /// <param name="personGrossSalary"></param>
        /// <returns>Net Salary</returns>
        public decimal GetNetSalary(decimal personGrossSalary)
        {
            const int roundPrecision = 2;

            if (personGrossSalary <= minimalTaxTreshold)
            {
                return personGrossSalary;
            }

            var midPointRound = MidpointRounding.AwayFromZero; // When working with money AwayFromZero tend to be the most precies rounding, compared to MS default .ToEven as it won't round up to 3 decimals when having x.3445.
                                                               // Not sure whether a round up to 3 decimals and then to 2 decimals would be better when calculating taxes.

            var incomeTax = decimal.Round(GetIncomeTax(personGrossSalary), roundPrecision, midPointRound); // Given tax is not payed in fraction of cents I'm rounding to 2 decimal places using MidpointRounding.AwayFromZero
            var socialTax = decimal.Round(GetSocialTax(personGrossSalary), roundPrecision, midPointRound); // Given tax is not payed in fraction of cents I'm rounding to 2 decimal places using MidpointRounding.AwayFromZero

            return decimal.Round(personGrossSalary, roundPrecision, midPointRound) - incomeTax - socialTax; // Given salary is not payed in fractions of cents (unless it's crypto :D ) I'm returning salary rounded to 2 decimals
                                                                                                            // and MidpointRounding.AwayFromZero.
        }

        private static decimal GetIncomeTax(decimal personGrossSalary)
        {
            const decimal incomeTaxPercent = (decimal)0.1; // Spending a little bit more memory for less CPU calculation.

            var taxableIncomeAmount = personGrossSalary - minimalTaxTreshold;
            return taxableIncomeAmount * incomeTaxPercent;
        }

        private static decimal GetSocialTax(decimal personGrossSalary)
        {
            const decimal socialTaxPercent = (decimal)0.15; // Spending a little bit more memory for less CPU calculation.
            const short maximalSocialTreshold = 3000;

            var maximumTaxableAmount = personGrossSalary > maximalSocialTreshold ? maximalSocialTreshold : personGrossSalary;
            var taxSocialAmount = maximumTaxableAmount - minimalTaxTreshold;

            return taxSocialAmount * socialTaxPercent;
        }
    }
}
