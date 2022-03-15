namespace Blog_Site.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Register(User user);
        public Task<User> Login(string username, string password);
        public Task<User> Edit(User user);

    }
}
