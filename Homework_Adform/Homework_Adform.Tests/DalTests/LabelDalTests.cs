using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.DAL;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.DalTests
{
    /// <summary>
    /// Label dal layer tests.
    /// </summary>
    public class LabelDalTests : BaseDBContextInitiator
    {
        private ILabelDalLayer _labelDalLayer;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _labelDalLayer = new LabelDalLayer(Mapper, DBContext);
        }


        /// <summary>
        /// Get labels test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabels()
        {
            var labels = await _labelDalLayer.GetLabels(1);
            int count = labels.Count;
            Assert.IsNotNull(labels);
            Assert.AreEqual(10, count);
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabel()
        {
            var addedLabel = await _labelDalLayer.Add(new CommonLibrary.Models.DTOs.CreateLabelDTO { Description ="Test label" });
            Assert.IsNotNull(addedLabel);
            Assert.AreEqual("Test label", addedLabel.Description);
        }
    }
}
