using System.ComponentModel.DataAnnotations;
using Task.Api.Entites.Shared;

namespace Task.Api.Entites
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; } = "";
        [Required]
        public string NameEn { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string CountryCode { get; set; } = "";

        [Required]
        [RegularExpression("^[01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4}$", ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NationalId { get; set; }
        [Required]
        public Statues MaritalStatus { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string AddressAr { get; set; } = "";
        [Required]
        public string AddressEn { get; set; } = "";
        public bool IsDeleted { get; set; } = false;

        //1-M relationship 
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
//[Required(ErrorMessage = "National ID is required")]
//[RegularExpression(@"^\d+$", ErrorMessage = "National ID must be a numeric value")]