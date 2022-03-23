namespace Blog_Site.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> RegisterAsync(User user);
        public Task<UserCon> LoginAsync(UserCon user);
        public Task<User> EditNameAsync(User userReq);
        public Task<User> EditPasswordAsync(User userReq);

    }
}
