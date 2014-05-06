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
        private WebPageProcessing _wpp = new WebPageProcessing(_testDirectory);
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
        public void AddResultsToListTest()
        {

            _wpp.AddResultsToList(_targetFile);
            _testResults = _wpp.GetResultList();

            _logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>> Number of items in memory: " + _testResults.Count);
            _logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>> First item in memory: " + _testResults[0]);
            _logger.Info(">>>>>>>>>>>>>>>>>>>>>>>>> Second item in memory: " + _testResults[1]);
            string tstString = "1956 View of the METROPOLITAN STADIUM in Progress  Press Photo";

            Assert.AreEqual(_testResults[_resultIndex], tstString);
        }

        [Test]
        public void RetrieveAndWriteWebPageToFileTest()
        {
            _logger.Info("Size of result list: " + _testResults.Count);

            // index numbers
            int oddNumber1 = 9;
            int oddNumber2 = 23;
            int oddNumber3 = 19;

            // files
            _wpp.RetrieveAndWriteWebPageToFile(_testResults[oddNumber1]);
            _wpp.RetrieveAndWriteWebPageToFile(_testResults[oddNumber2]);
            _wpp.RetrieveAndWriteWebPageToFile(_testResults[oddNumber3]);

            Assert.AreEqual(1, 1);
        }

        [Test]
        public void UtilitiesGenerateRandomStringTest()
        {
            string randString = EBAY_PHOTO_HARVESTER.Utilities.GenerateRandomString();
            _logger.Info("Generated random string: " + randString);

            Assert.AreEqual(12, randString.Length);
        }
    }
}
