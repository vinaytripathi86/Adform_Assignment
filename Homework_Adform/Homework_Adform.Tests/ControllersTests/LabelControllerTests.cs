using Homework_Adform.CommonLibrary.Models.Requests;
using Homework_Adform.TodoAPI.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ControllersTests
{
    /// <summary>
    /// Label controller tests.
    /// </summary>
    public class LabelControllerTests : BaseController
    {
        private LabelsController controller;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            controller = new LabelsController(LabelLogger.Object, LabelService.Object, Mapper)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            var result = await controller.Post(new CreateLabelRequest { Description = "test" }, Version);
            var response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            var result = await controller.Delete(new DeleteLabelRequest { Id = 1 });
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelTest()
        {
            var result = await controller.Get(1);
            var response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
