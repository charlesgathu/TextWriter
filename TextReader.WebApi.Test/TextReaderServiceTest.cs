using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using TextReader.Managers;
using TextReader.WebApi.Controllers;

namespace TextReader.WebApi.Test
{
    [TestClass]
    public class TextReaderServiceTest
    {

        [TestMethod]
        public void TestSort()
        {
            var manager = new Mock<ITextReaderManager>();
            manager.Setup(mn => mn.Sort("", Managers.SortOption.Ascending)).Returns("");
            var controller = new TextController(manager.Object);

            var response = controller.Sort(new Models.TextSortItem { SortOption = SortOption.Ascending, Text = "" }) as ObjectResult;

            Assert.IsTrue(response.StatusCode.Value == (int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestStatistics()
        {
            var manager = new Mock<ITextReaderManager>();
            manager.Setup(mn => mn.GetStatistics("")).Returns(new TextStatistics { Hyphens = 10, Spaces = 0, Words = 30 });
            var controller = new TextController(manager.Object);

            var response = controller.Statistics(new Models.TextStatisticsItem { Text = "This is some long text" }) as ObjectResult;

            Assert.IsTrue(response.StatusCode.Value == (int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestErrorStatistics()
        {
            var manager = new Mock<ITextReaderManager>();
            manager.Setup(mn => mn.GetStatistics("This is some long text")).Throws(new System.Exception());
            var controller = new TextController(manager.Object);

            var response = controller.Statistics(new Models.TextStatisticsItem { Text = "This is some long text" }) as StatusCodeResult;

            Assert.IsTrue(response.StatusCode == (int)HttpStatusCode.InternalServerError);
        }

    }
}
