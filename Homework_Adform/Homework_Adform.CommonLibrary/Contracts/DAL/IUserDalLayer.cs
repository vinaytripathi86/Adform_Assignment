using Homework_Adform.CommonLibrary.Models;
using System.Threading.Tasks;

namespace Homework_Adform.CommonLibrary.Contracts.DAL
{
    /// <summary>
    /// Contract for user data layer.
    /// </summary>
    public interface IUserDalLayer
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="Password">Password.</param>
        /// <returns>Returns user id.</returns>
        Task<long?> AuthenticateUser(string userName, string Password);

        /// <summary>
        /// Get user by user id.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>Returns user details.</returns>
        Task<UserModel> GetById(long userId);
    }
}
