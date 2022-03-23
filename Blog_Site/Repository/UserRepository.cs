using Blog_Site.Data;
using Blog_Site.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Blog_Site.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UserRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<User> EditNameAsync(User userReq)
        {
            User user = await _context.Users.Where(u => u.Id == userReq.Id).FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }

            user.UserName = userReq.UserName;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> EditPasswordAsync(User userReq)
        {
            User user = await _context.Users.Where(u => u.Id == userReq.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            string hashedPW = passwordHash(userReq.Password, user.PasswordSalt);
            user.Password = hashedPW;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserCon> LoginAsync(UserCon userCon)
        {
            User user = await _context.Users.Where(u => u.UserName == userCon.UserName).FirstOrDefaultAsync();

            if (user != null)
            {
                string hashedPW = passwordHash(userCon.Password, user.PasswordSalt);
                if (hashedPW == user.Password)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Type)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(2),
                        signingCredentials: signIn);

                    string strToken = new JwtSecurityTokenHandler().WriteToken(token);
                    userCon.Token = strToken;
                    userCon.Password = "";

                    return userCon;
                }
            }
            
            return null;
        }

        public async Task<User> RegisterAsync(User user)
        {
            User exUser = await _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
            if (exUser == null)
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetNonZeroBytes(salt);
                }
                string hashedPW = passwordHash(user.Password, salt);
                user.Password = hashedPW;
                user.PasswordSalt = salt;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return user; 
            }

            return null;
        }


        private string passwordHash(string password, byte[] salt)
        {
            string hashedPW = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));

            return hashedPW;
        }
    }
}
