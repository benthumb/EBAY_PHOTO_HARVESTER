using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NLog;

namespace EBAY_PHOTO_HARVESTER
{
  [TestFixture]
  public class WebPageProcessingTest
  {
    private const string _targetFile = TransactionSettings.BaseDirectory + "data\\photographs_1922.txt";
    private const string _testDirectory = TransactionSettings.BaseDirectory;
    private WebPageProcessing _wpp = new WebPageProcessing(_testDirectory);
    private const int _resultIndex = 30;
    private List<string> _testResults = new List<string>();
    private static readonly Logger _logger = LogManager.GetLogger("EBAY_PHOTO_HARVESTER.WebPageProcessingTest");

    [TestFixtureSetUp]
    public void SetUpLogging()
    {
      // unecessary ... for the time being
    }

    [TestFixtureTearDown]
    public void CleanUpTestFiles()
    {
      try
      {
        string[] webPageList = Directory.GetFiles(TransactionSettings.SavedWebPagesDirectory, "*.html");
        foreach (string f in webPageList)
        {
          //File.Delete(f);
        }
      }
      catch (DirectoryNotFoundException dirNotFound)
      {
        _logger.Error("Routine failed with following error message: " + dirNotFound.Message);
      }
    }

    [Test]
    public void AddSearchResultsToListTest()
    {

      _wpp.AddSearchResultsToList(_targetFile);
      _testResults = _wpp.GetResultList();

      _logger.Info("Number of items in memory: " + _testResults.Count);
      _logger.Info("First item in memory: " + _testResults[0]);
      _logger.Info("Second item in memory: " + _testResults[1]);
      string tstString = "Seven Nurses in Uniforms and Caps Class of 1922 Vintage Photo Snapshot";

      Assert.AreEqual(_testResults[_resultIndex], tstString);
    }

    [Test]
    public void RetrieveAndWriteWebPageToFileTest()
    {
      // test size of file
      long fileSize = 0;

      _logger.Info("Size of result list: " + _testResults.Count);

      // index numbers
      int oddNumber1 = 15;
      int oddNumber2 = 77;
      int oddNumber3 = 93;

      // files
      _wpp.RetrieveAndWriteWebPageToFile(_testResults[oddNumber1]);
      _wpp.RetrieveAndWriteWebPageToFile(_testResults[oddNumber2]);
      _wpp.RetrieveAndWriteWebPageToFile(_testResults[oddNumber3]);

      string[] webPageList = Directory.GetFiles(TransactionSettings.SavedWebPagesDirectory, "*.html");

      // Full file name 
      foreach (string testWebFile in webPageList)
      {
        string fileName = testWebFile;
        FileInfo fi = new FileInfo(fileName);
        fileSize = fi.Length;

        _logger.Info("File Size in Bytes: " + fileSize);
      }

      Assert.AreEqual(1, 1);
    }

    [Test]
    public void GenerateRandomStringUtilityTest()
    {
      string randString = EBAY_PHOTO_HARVESTER.Utilities.GenerateRandomString();
      _logger.Info("Generated random string: " + randString);
      _logger.Info("Random bullshite: fuck u very much!");

      Assert.AreEqual(12, randString.Length);
    }

    [Test]
    public void GetMySQLConnectionTest()
    {
      DataStoreConnectivity dsc = new DataStoreConnectivity();
      dsc.GetMySQLConnection();
      Assert.AreEqual(1, 1);
    }

    [Test]
    public void ParseWebPageExtractJPGTest()
    {
      string testHTML = @"<html><head><title>WHAT?</title></head><body><img id=""icThrImg"" class=""img img300 vi-hide-mImgThr"" style="""" src=""http://p.ebaystatic.com/aw/pics/globalAssets/imgLoading_30x30.gif"" imgsel=""0"" alt=""Italian-Army-Regio-Esercito-Soldiers-photo-1922"" /></body></html>";
      _wpp.ParseWebPageExtractJPG(testHTML);

      Assert.AreEqual(1, 1);
    }
  }
}
