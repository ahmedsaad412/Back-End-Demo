namespace Task.Api.DTO
{
    public class UsersDTO
    {
        // Columns for full name, email, mobile number, job name , and department name

        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string JobName { get; set; }
        public string DepartmentName { get; set; }
    }
}
