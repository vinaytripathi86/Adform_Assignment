using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ServicesTests
{
    /// <summary>
    /// User service tests.
    /// </summary>
    public class UserServiceTests
    {
        private Mock<IUserDalLayer> _userDalLayer;
        private IUserService _userService;

        /// <summary>
        /// Set up.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _userDalLayer = new Mock<IUserDalLayer>();
            _userService = new UserService(_userDalLayer.Object);
        }

        /// <summary>
        /// Auth valid test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authenticate_ValidUserNameAndPassword()
        {
            _userDalLayer.Setup(p => p.AuthenticateUser(string.Empty, string.Empty)).Returns(Task.FromResult<long?>(1));
            long? userId = await _userService.AuthenticateUser(string.Empty, string.Empty);
            Assert.IsTrue(userId.HasValue);
            Assert.AreEqual(1, userId.Value);
        }

        /// <summary>
        /// Auth invalid test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authenticate_InvalidUserNameAndPassword()
        {
            _userDalLayer.Setup(p => p.AuthenticateUser(string.Empty, string.Empty)).Returns(Task.FromResult<long?>(null));
            long? userId = await _userService.AuthenticateUser(string.Empty, string.Empty);
            Assert.IsTrue(!userId.HasValue);
        }
    }
}
