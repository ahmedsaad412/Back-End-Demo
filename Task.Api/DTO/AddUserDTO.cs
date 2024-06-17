using System.ComponentModel.DataAnnotations;
using Task.Api.Entites.Shared;

namespace Task.Api.DTO
{
    public class AddUserDTO
    {
        [Required]
        public string FNameAr { get; set; } = "";
        [Required]
        public string SecNameAr { get; set; } = "";
        [Required]
        public string ThirdNameAr { get; set; } = "";
        [Required]
        public string LNameAr { get; set; } = "";
        [Required]
        public string FNameEn { get; set; } = "";
        [Required]
        public string SecNameEn { get; set; } = "";
        [Required]
        public string ThirdNameEn { get; set; } = "";
        [Required]
        public string LNameEn { get; set; } = "";
        [Required]

        public string PhoneNumber { get; set; }
        [Required]
        public string CountryCode { get; set; } = "";
        [Required]
        [Range(0, int.MaxValue)]
        public int NationalId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Statues MaritalStatus { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string AddressAr { get; set; } = "";
        [Required]
        public string AddressEn { get; set; } = "";
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
    }
}
