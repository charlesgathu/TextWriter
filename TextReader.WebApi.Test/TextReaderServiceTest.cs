using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Net;
using TextReader.Managers;
using TextReader.WebApi.Controllers;

namespace TextReader.WebApi.Test
{
    [TestClass]
    public class TextReaderServiceTest
    {

        private Mock<ITextReaderManager> manager;
        private ILogger logger;

        [TestInitialize]
        public void Init()
        {
            manager = new Mock<ITextReaderManager>();
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("Console")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            config.AddRuleForAllLevels(consoleTarget);
            LogManager.Configuration = config;

            // Example usage
            logger = LogManager.GetLogger("Console");

        }

        [TestMethod]
        public void TestSort()
        {
            manager.Setup(mn => mn.Sort("", Managers.SortOption.Ascending)).Returns("");
            var controller = new TextController(manager.Object, logger);

            var response = controller.Sort(new Models.TextSortItem { SortOption = SortOption.Ascending, Text = "" }) as ObjectResult;

            Assert.IsTrue(response.StatusCode.Value == (int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestStatistics()
        {
            manager.Setup(mn => mn.GetStatistics("")).Returns(new TextStatistics { Hyphens = 10, Spaces = 0, Words = 30 });
            var controller = new TextController(manager.Object, logger);

            var response = controller.Statistics(new Models.TextStatisticsItem { Text = "This is some long text" }) as ObjectResult;

            Assert.IsTrue(response.StatusCode.Value == (int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestErrorStatistics()
        {
            manager.Setup(mn => mn.GetStatistics("This is some long text")).Throws(new System.Exception());
            var controller = new TextController(manager.Object, logger);

            var response = controller.Statistics(new Models.TextStatisticsItem { Text = "This is some long text" }) as StatusCodeResult;

            Assert.IsTrue(response.StatusCode == (int)HttpStatusCode.InternalServerError);
        }

    }
}
