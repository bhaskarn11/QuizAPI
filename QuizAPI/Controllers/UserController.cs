using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Schemas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User? Get(int id)
        {

            return context.Users.Where(p => p.Id == id).FirstOrDefault(); ;
        }

        
        [HttpPost]
        public User Post(CreateUser createUser)
        {
            User user = mapper.Map<User>(createUser);
            context.Add(user);
            context.SaveChanges();

            return user;
        }


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            User? user = context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user == null)
            {
                return 0;
            }
            context.Remove(user);
            return context.SaveChanges();
        }

        [HttpPut]
        public User? Update(UpdateUser updateUser)
        {
            User? user = context.Users.Where(p => p.Id == updateUser.userId).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.IsDisabled = updateUser.IsDisabled;
            
            context.SaveChanges();
            return user;
        }
    }
}
