using Kryptonite.Application.Interfaces;
using Kryptonite.Domain.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Kryptonite.API.Controllers {
    public class UserController : BaseController {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }
       [HttpPost]
        public async Task<User> Login(string loginName , string loginPassword) {
            return await _userService.Login(loginName, loginPassword);
        }
    }
}
