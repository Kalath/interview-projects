namespace Invelop.Project.Core.Models
{
    public class PersonContacts
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IBAN { get; set; }
    }
}
