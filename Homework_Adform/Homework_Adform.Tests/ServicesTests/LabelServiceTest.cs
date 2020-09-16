using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ServicesTests
{
    /// <summary>
    /// Label service tests.
    /// </summary>
    public class LabelServiceTest
    {
        private Mock<ILabelDalLayer> _labelDalLayer;
        private ILabelService _labelService;
        private LabelDto _labelDto = new LabelDto { Id = 1, Description = "test" };

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _labelDalLayer = new Mock<ILabelDalLayer>();
            _labelService = new LabelService(_labelDalLayer.Object);
            _labelDalLayer.Setup(p => p.Add(It.IsAny<CreateLabelDTO>())).Returns(Task.FromResult(_labelDto));
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            var result = await _labelService.Add(new CreateLabelDTO());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }
    }
}
