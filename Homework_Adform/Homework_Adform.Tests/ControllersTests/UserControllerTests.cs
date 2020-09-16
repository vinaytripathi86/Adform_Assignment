using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.TodoAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ControllersTests
{
    /// <summary>
    /// User controller tests.
    /// </summary>
    public class UserControllerTests : BaseController
    {
        private UserController controller;
        private IOptions<AppSettings> options;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            options = Options.Create(new AppSettings { Secret = "this is my custom Secret key for authnetication" });
            controller = new UserController(UserLogger.Object, UserService.Object, options)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Authentication test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AuthenticateTest()
        {
            var result = await controller.Authenticate(new CommonLibrary.Models.Requests.LoginRequest { UserName = "1" });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
