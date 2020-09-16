using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.Requests;
using Homework_Adform.TodoAPI.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ControllersTests
{
    /// <summary>
    /// Items controller tests.
    /// </summary>
    public class ItemsControllerTests : BaseController
    {
        private ItemsController controller;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            controller = new ItemsController(ItemsLogger.Object, ItemsService.Object, Mapper)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddItemTest()
        {
            var result = await controller.Post(new CreateItemRequest { Note = "test" }, Version);
            var response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Update item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateItemTest()
        {
            var result = await controller.Put(new UpdateItemRequest { Id = 1, Note = "test" });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            var result = await controller.Delete(new DeleteItemRequest { Id = 1, ListId = 1 });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetItemTest()
        {
            var result = await controller.GetItem(new GetItemRequest { ListId = 1, Id = 1 });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
