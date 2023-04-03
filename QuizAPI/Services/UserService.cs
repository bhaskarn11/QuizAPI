using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace QuizAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        
        public User? GetUserById(int id)
        {

            User? user = context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("No user found");
            }

            return user;
        }


        [HttpPost]
        public User CreateUser(CreateUser createUser)
        {
            User user = mapper.Map<User>(createUser);
            context.Add(user);
            context.SaveChanges();

            return user;
        }


        public int DeleteUser(int id)
        {
            User? user = context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("No user found");
            }
            context.Remove(user);
            return context.SaveChanges();
        }

       
        public User UpdateUser(UpdateUser updateUser)
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

        public User LogIn()
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

    }
}
