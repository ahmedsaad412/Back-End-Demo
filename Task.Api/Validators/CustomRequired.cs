using System.ComponentModel.DataAnnotations;

namespace Task.Api.Validators
{
    public class CustomRequired : ValidationAttribute
    {
        public string SepcialType { get; private set; }
        public CustomRequired(string sepcialType)
        {
            this.SepcialType = sepcialType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (value == null)
            {
                errorType = "required";
            }
            else if (!string.IsNullOrEmpty(SepcialType) && !IsValidEmail(value.ToString()))
            {
                errorType = "not valid";
            }
            else
            {
                return ValidationResult.Success;
            }
            ErrorMessage = $"{validationContext.DisplayName}  field is {errorType}.";
            return new ValidationResult(ErrorMessage);
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
