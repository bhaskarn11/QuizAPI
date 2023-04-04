using QuizAPI.Schemas.Users;

namespace QuizAPI.Services.Interfaces
{
    public interface IUserService
    {
        public User? GetUserById(int userId);
        public int DeleteUser(int userId);
        public User CreateUser(CreateUser createUser);
        public User UpdateUser(UpdateUser updateUser);
        public TokenResponse LogIn(LoginRequest loginRequest);
        public void LogOut(int userId);

    }
}
