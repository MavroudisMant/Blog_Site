using Blog_Site.Data;
using Blog_Site.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Blog_Site.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Task<User> Edit(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user)
        {
            (string hashedPW, byte[] salt) = passwordHash(user.Password);
            user.Password = hashedPW;
            user.PasswordSalt = salt;
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        private (string, byte[]) passwordHash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(salt);
            }


            string hashedPW = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));

            return (hashedPW, salt);
        }
    }
}
