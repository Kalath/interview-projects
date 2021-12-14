namespace DS.TaxCalculator.Models
{
    public class PersonTax
    {
        public decimal GrossSalary { get; set; }
        public decimal TaxableAmmount { get; set; }
        public decimal SocialContribution { get; set; }
        public decimal NetSalary { get; set; }
    }
}