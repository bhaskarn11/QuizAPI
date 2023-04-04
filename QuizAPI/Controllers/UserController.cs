using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Schemas.Users;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
       
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            
            this.userService = userService;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public Response<User?> Get(int id)
        {

            try
            {
                var u = userService.GetUserById(id);
                return new Response<User?>(u, true);
            }
            catch (Exception e)
            {
                return new Response<User?>(null, false, e.Message);
            }
        }

        
        [HttpPost, AllowAnonymous]
        public Response<User?> Post(CreateUser createUser)
        {
            try
            {
                var u = userService.CreateUser(createUser);
                return new Response<User?>(u, true);
            }
            catch (Exception e)
            {
                return new Response<User?>(null, false, e.Message);
            }
        }


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public Response<int?> Delete(int id)
        {
            try
            {
                var u = userService.DeleteUser(id);
                return new Response<int?>(u, true);
            }
            catch (Exception e)
            {
                return new Response<int?>(null, false, e.Message);
            }
        }

        [HttpPut]
        public Response<User?> Update(UpdateUser updateUser)
        {
            try
            {
                var u = userService.UpdateUser(updateUser);
                return new Response<User?>(u, true);
            }
            catch (Exception e)
            {
                return new Response<User?>(null, false, e.Message);
            }
        }

        [HttpPost("Login"), AllowAnonymous]
        public TokenResponse Login(LoginRequest loginRequest)
        {
            try
            {
                return userService.LogIn(loginRequest);
            }
            catch (Exception e)
            {
                return new TokenResponse(false, e.Message);
            }
        }
    }
}
