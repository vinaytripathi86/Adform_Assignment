using Homework_Adform.CommonLibrary.Models.Requests;
using Homework_Adform.TodoAPI.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ControllersTests
{
    /// <summary>
    /// Lists controller tests.
    /// </summary>
    public class ListsControllerTests : BaseController
    {
        private ListsController controller;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            controller = new ListsController(ListLogger.Object, ListService.Object, Mapper)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddListTest()
        {
            var result = await controller.Post(new CreateListRequest { Description ="test" }, Version);
            var response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Update list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateListTest()
        {
            var result = await controller.Put(new UpdateListRequest { ListId = 1, Description = "test" });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteListTest()
        {
            var result = await controller.Delete(new DeleteListRequest { Id = 1 });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetListTest()
        {
            var result = await controller.GetList(1);
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
