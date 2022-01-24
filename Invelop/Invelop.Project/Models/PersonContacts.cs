namespace Invelop.Project.Client.Models
{
    public class PersonContacts
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateOnly Dob { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string IBAN { get; set; }
    }
}
