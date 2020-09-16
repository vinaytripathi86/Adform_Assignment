using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.DAL.DBContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Adform.DAL
{
    /// <summary>
    /// Implemenation of IUserDalLayer contract.
    /// </summary>
    public class UserDalLayer : IUserDalLayer
    {        
        private readonly HomeworkDBContext _dbContext;

        /// <summary>
        /// Create new instance of <see cref="UserDalLayer"/> class.
        /// </summary>
        /// <param name="dBContext">Db context.</param>
        public UserDalLayer(HomeworkDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="Password">Password.</param>
        /// <returns>Returns user id.</returns>
        public async Task<long?> AuthenticateUser(string userName, string password)
        {
            var user = await _dbContext.Users.Where(p => p.Username == userName && p.Password == password).SingleOrDefaultAsync();
            if (null == user) return null;
            return user.Id;
        }

        /// <summary>
        /// Get user by user id.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>Returns user details.</returns>
        public async Task<UserModel> GetById(long userId)
        {
            var user = await _dbContext.Users.Where(p => p.Id == userId).SingleOrDefaultAsync();
            if (null == user) return null;
            return new UserModel { UserId = user.Id, UserName = user.Username };
        }
    }
}
