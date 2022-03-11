using Kryptonite.Domain.Entities;

namespace Kryptonite.Application.Interfaces {
    public interface IUserService {
        public Task<User> Login(string loginName, string loginPassword);
    }
}
