using System.ComponentModel.DataAnnotations;

namespace Invelop.Project.Client.Models
{
    public class PersonContactsViewModel
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(60)]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(60)]
        public string Surname { get; set; }

        //Would be good to have custom validator (derived from ValidationAttribute) for DoB to not be after Today
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(30)]
        public string? PhoneNumber { get; set; }

        [StringLength(34)]
        public string? IBAN { get; set; }
    }
}
