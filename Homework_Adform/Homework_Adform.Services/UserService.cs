using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using System.Threading.Tasks;

namespace Homework_Adform.Services
{
    /// <summary>
    /// Implemenation of IUserService contract.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserDalLayer _userDalLayer;

        /// <summary>
        /// Create new instance of <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDalLayer">User dal layer.</param>
        public UserService(IUserDalLayer userDalLayer)
        {
            _userDalLayer = userDalLayer;
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="Password">Password.</param>
        /// <returns>Returns user id.</returns>
        public async Task<long?> AuthenticateUser(string userName, string Password) => await _userDalLayer.AuthenticateUser(userName, Password);

        /// <summary>
        /// Get user by user id.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>Returns user details.</returns>
        public async Task<UserModel> GetById(long userId) => await _userDalLayer.GetById(userId);
    }
}
