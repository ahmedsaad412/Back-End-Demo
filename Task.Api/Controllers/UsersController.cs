using Microsoft.AspNetCore.Mvc;
using Task.Api.DTO;
using Task.Api.Entites;
using Task.Api.IServices;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, UserDetailsDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _userService.EditUser(id, user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] AddUserDTO userDto)
        {
            try
            {
                #region validations
                //var validator = new UserValidator();
                //var ModelState = validator.Validate(userDto);
                //if (!ModelState.IsValid) return BadRequest(ModelState);
                #endregion



                if (await _userService.AddUser(userDto))
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch
            {
                return BadRequest(false);
            }

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(user.Id);
            return Ok();
        }


        [HttpGet("GetDepartments")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetDepartments()
        {
            var departments = await _userService.GetDepartments();
            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        [HttpGet("GetJobs")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetJobs()
        {
            var jobs = await _userService.GetJobs();
            if (jobs == null)
            {
                return NotFound();
            }

            return Ok(jobs);
        }
    }
}
