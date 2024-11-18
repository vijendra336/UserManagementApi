using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementApi.Data;
using UserManagementApi.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: UserController
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUser()
        {
            var users = await dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("page/{pageNumber}/{pageSize}")]
        public async Task<ActionResult> GetUserByPageNo(int pageNumber,int pageSize)
        {
            var users = await dbContext.Users
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalUsers = await dbContext.Users.CountAsync();  // Optional: get the total count of users

            var response = new PaginatedUsersResponse
            {
                Users =  users,
                Total = totalUsers  // Include total count if needed
            };


            return Ok(response);
        }



        // POST: UserController/Create
        [HttpPost]
      
        public  async Task<ActionResult> CreateUser([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user==null || string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName)
                || user.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                return BadRequest("Invalid User data");
            }

            if (user.DateOfBirth.Kind != DateTimeKind.Utc)
            {
                user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc);
            }


            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok("User added sucessfully");
        }

       

      
    }
}
