using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.Schemas.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace QuizAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UserService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
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
            GetPasswordHash(createUser.Password!, out string passwordHash);
            user.HashedPassword = passwordHash;
            // user.PasswordSalt = salt;
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


        public TokenResponse LogIn(LoginRequest loginRequest)
        {
            User? u = context.Users.Where(p => p.Email == loginRequest.Email).FirstOrDefault();
            if (u == null)
            {
                throw new Exception("User not found");
            }

            if (!VerifyPasswordHash(loginRequest.Password, u.HashedPassword!))
            {
                throw new Exception("User access denied");
            }

            return CreateToken(u);
        }


        public void LogOut(int userId)
        {
            throw new NotImplementedException();
        }


        private void GetPasswordHash(string password, out string passwordHash)
        {
            var hmac = SHA256.Create();
            
            // byte[] hashKey = hmac.Key;
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordHash = Encoding.UTF8.GetString(hash);
            // salt = Convert.ToHexString(hashKey);
           
        }


        private TokenResponse CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                 new Claim(ClaimTypes.Email, user.Email!),
                 new Claim(ClaimTypes.Role, user.UserType.ToString()!),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secrets:Key").Value!));
            
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: credential
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse(jwt, true, "");
        }


        private bool VerifyPasswordHash(string password, string hashedPassword)
        {
            
            var hmac = SHA256.Create();
            
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(computedHash) == hashedPassword;
            
        }

    }
}
