using Kryptonite.Domain.Entities;
namespace Kryptonite.Domain.Interfaces {
    public interface IUserRepository {
        public Task<User> Login(string loginName, string loginPassword);
    }
}
