using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Slf;
using Slf.BitFactoryFacade;

namespace ConsoleFindItems
{
    [TestFixture]
    public class WebPageProcessingTest
    {
        private const string _targetFile = TransactionSettings.BaseDirectory + "data\\photographs_1956.txt";
        private const string _testDirectory = TransactionSettings.BaseDirectory;
        private const int _resultIndex = 8;
        private List<string> _testResults = new List<string>();
        private ILogger _logger;

        [TestFixtureSetUp]
        public void SetUpLogging()
        {
            LoggerService.SetLogger(new ConsoleLogger());
            _logger = LoggerService.GetLogger();
        }

        [Test]
        public void CommittResultsToMemoryTest()
        {
            WebPageProcessing wpp = new WebPageProcessing(_testDirectory);
            wpp.AddResultsToList(_targetFile);
            _testResults = wpp.GetResultList();

            _logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>> Number of items in memory: " + _testResults.Count);
            _logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>> First item in memory: " + _testResults[0]);
            _logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>> Second item in memory: " + _testResults[1]);
            string tstString = "1956 View of the METROPOLITAN STADIUM in Progress  Press Photo";

            Assert.AreEqual(_testResults[_resultIndex], tstString);
        }
    }
}
