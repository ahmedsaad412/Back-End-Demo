using Microsoft.EntityFrameworkCore;
using Task.Api.Data;
using Task.Api.DTO;
using Task.Api.IServices;

namespace Task.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(AddUserDTO AddUserDto)
        {
            try
            {
                await _context.Users.AddAsync(new()
                {
                    NameAr = string.Concat(AddUserDto.FNameAr, "#", AddUserDto.SecNameAr, "#", AddUserDto.ThirdNameAr, "#", AddUserDto.LNameAr),
                    NameEn = string.Concat(AddUserDto.FNameEn, "#", AddUserDto.SecNameEn, "#", AddUserDto.ThirdNameEn, "#", AddUserDto.LNameEn),
                    CountryCode = AddUserDto.CountryCode,
                    PhoneNumber = AddUserDto.PhoneNumber,
                    Email = AddUserDto.Email,
                    NationalId = AddUserDto.NationalId,
                    BirthDate = AddUserDto.BirthDate,
                    Gender = AddUserDto.Gender,
                    MaritalStatus = AddUserDto.MaritalStatus,
                    AddressAr = AddUserDto.AddressAr,
                    AddressEn = AddUserDto.AddressEn,
                    JobId = AddUserDto.JobId,
                    DepartmentId = AddUserDto.DepartmentId,
                    IsDeleted = false,
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<bool> EditUser(int id, UserDetailsDTO userDetailsDTO)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    return false;

                #region NameValue 
                string fNameAr = userDetailsDTO.FNameAr == "string" ? GetSegment(user.NameAr, 0) : userDetailsDTO.FNameAr;
                string secNameAr = userDetailsDTO.SecNameAr == "string" ? GetSegment(user.NameAr, 1) : userDetailsDTO.SecNameAr;
                string thirdNameAr = userDetailsDTO.ThirdNameAr == "string" ? GetSegment(user.NameAr, 2) : userDetailsDTO.ThirdNameAr;
                string lNameAr = userDetailsDTO.LNameAr == "string" ? GetSegment(user.NameAr, 3) : userDetailsDTO.LNameAr;
                string fNameEn = userDetailsDTO.FNameEn == "string" ? GetSegment(user.NameEn, 0) : userDetailsDTO.FNameEn;
                string secNameEn = userDetailsDTO.SecNameEn == "string" ? GetSegment(user.NameEn, 1) : userDetailsDTO.SecNameEn;
                string thirdNameEn = userDetailsDTO.ThirdNameEn == "string" ? GetSegment(user.NameEn, 2) : userDetailsDTO.ThirdNameEn;
                string lNameEn = userDetailsDTO.LNameEn == "string" ? GetSegment(user.NameEn, 3) : userDetailsDTO.LNameEn;
                #endregion

                user.NameAr = string.Concat(fNameAr, "#", secNameAr, "#", thirdNameAr, "#", lNameAr);
                user.NameEn = string.Concat(fNameEn, "#", secNameEn, "#", thirdNameEn, "#", lNameEn);
                user.CountryCode = userDetailsDTO.CountryCode == "string" ? user.CountryCode : userDetailsDTO.CountryCode;
                user.PhoneNumber = userDetailsDTO.PhoneNumber == "string" ? user.PhoneNumber : userDetailsDTO.PhoneNumber;
                user.Email = userDetailsDTO.Email == "string" ? user.Email : userDetailsDTO.Email;
                user.NationalId = userDetailsDTO.NationalId == 0 ? user.NationalId : userDetailsDTO.NationalId;
                user.BirthDate = userDetailsDTO.BirthDate == DateTime.Now ? user.BirthDate : userDetailsDTO.BirthDate;
                user.Gender = userDetailsDTO.Gender == 0 ? user.Gender : userDetailsDTO.Gender;
                user.MaritalStatus = userDetailsDTO.MaritalStatus == 0 ? user.MaritalStatus : userDetailsDTO.MaritalStatus;
                user.AddressAr = userDetailsDTO.AddressAr == "string" ? user.AddressAr : userDetailsDTO.AddressAr;
                user.AddressEn = userDetailsDTO.AddressEn == "string" ? user.AddressEn : userDetailsDTO.AddressEn;
                user.JobId = userDetailsDTO.JobId == 0 ? user.JobId : userDetailsDTO.JobId;
                user.DepartmentId = userDetailsDTO.DepartmentId == 0 ? user.DepartmentId : userDetailsDTO.DepartmentId;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<UserDetailsDTO> GetUserById(int Id)
        {
            var user = await _context.Users.Where(r => r.Id == Id)
                                .Select(r => new UserDetailsDTO
                                {
                                    Id = r.Id,
                                    Email = r.Email,
                                    FNameAr = GetSegment(r.NameAr, 0),
                                    SecNameAr = GetSegment(r.NameAr, 1),
                                    ThirdNameAr = GetSegment(r.NameAr, 2),
                                    LNameAr = GetSegment(r.NameAr, 3),
                                    PhoneNumber = r.PhoneNumber,
                                    FNameEn = GetSegment(r.NameEn, 0),
                                    SecNameEn = GetSegment(r.NameEn, 1),
                                    ThirdNameEn = GetSegment(r.NameEn, 2),
                                    LNameEn = GetSegment(r.NameEn, 3),
                                    CountryCode = r.CountryCode,
                                    AddressAr = r.AddressAr,
                                    AddressEn = r.AddressEn,
                                    JobId = r.JobId,
                                    DepartmentId = r.DepartmentId,
                                    NationalId = r.NationalId,
                                    BirthDate = r.BirthDate,
                                    Gender = r.Gender,
                                    MaritalStatus = r.MaritalStatus,

                                }).FirstOrDefaultAsync();

            return user;
        }



        public async Task<List<UsersDTO>> GetUsers()
        {
            var users = await _context.Users
                .Include(x => x.Department)
                .Include(x => x.Job)
                .Select(x => new UsersDTO
                {
                    Id = x.Id,
                    FullName = x.NameEn.Replace("#", " "),
                    Email = x.Email,
                    MobileNumber = x.PhoneNumber,
                    DepartmentName = x.Department.DepartmentName,
                    JobName = x.Job.JobName
                })
                .ToListAsync();

            return users;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return user.IsDeleted;
        }


        public async Task<List<DepartmentsDTO>> GetDepartments()
        {
            var departments = await _context.Departments
                .Select(x => new DepartmentsDTO
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName
                })
                .ToListAsync();
            return departments;
        }

        public async Task<List<JobsDTO>> GetJobs()
        {
            var Jobs = await _context.Jobs.
                Select(x => new JobsDTO { Id = x.Id, JobName = x.JobName })
                .ToListAsync();
            return Jobs;

        }
        public static string GetSegment(string fullName, int index)
        {
            var segments = fullName.Split("#");
            return index < segments.Length ? segments[index] : string.Empty;
        }
    }
}
