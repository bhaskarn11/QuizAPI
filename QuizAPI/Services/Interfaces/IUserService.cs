namespace QuizAPI.Services.Interfaces
{
    public interface IUserService
    {
        public User getUserById(int userId);
        public int DeleteUser(int userId);
        public User CreateUser(CreateUser createUser);
        public User UpdateUser(UpdateUser updateUser);
        public User LogIn();
        public void LogOut();

    }
}
