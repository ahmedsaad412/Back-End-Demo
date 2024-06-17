using Task.Api.DTO;

namespace Task.Api.IServices
{
    public interface IUserService
    {
        Task<List<UsersDTO>> GetUsers();
        Task<bool> AddUser(AddUserDTO AddUserDto);
        Task<UserDetailsDTO> GetUserById(int Id);
        Task<bool> EditUser(UserDetailsDTO userDetailsDTO);
        Task<bool> DeleteUser(int id);
        Task<List<DepartmentsDTO>> GetDepartments();
        Task<List<JobsDTO>> GetJobs();


    }
}
