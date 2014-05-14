using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using NLog;

namespace EBAY_PHOTO_HARVESTER
{
  class WebPageProcessing
  {
    private List<String> _resultList;
    private string _fileBase = TransactionSettings.BaseDirectory;
    private static readonly Logger _logger = LogManager.GetLogger("EBAY_PHOTO_HARVESTER.WebPageProcessing");

    public WebPageProcessing(string baseDirectory)
    {
      _fileBase = baseDirectory;
      _resultList = new List<String>();
      _logger.Info("Creating instance of WebPageProcessing class...");
    }

    public void AddSearchResultsToList(String filePath)
    {
      _logger.Info("Adding results to list...");
      string line;
      // read file
      StreamReader file = new StreamReader(filePath);
      while ((line = file.ReadLine()) != null)
      {
        Console.WriteLine(line);
        _resultList.Add(line);
        // add urls to String[] array
      }
    }

    public List<string> GetResultList()
    {
      return _resultList;
    }

    public void RetrieveAndWriteWebPageToFile(string urlString)
    {
      string htmlString = "";
      _logger.Info("Retrieving and writing web pages...");
      WebRequest wrGETURL;
      wrGETURL = WebRequest.Create(urlString);
      string targetFile = TransactionSettings.SavedWebPagesDirectory
          + EBAY_PHOTO_HARVESTER.Utilities.GenerateRandomString()
          + ".html";
      //string htmlString = "";

      Stream objStream;
      objStream = wrGETURL.GetResponse().GetResponseStream();
      StreamReader objReader = new StreamReader(objStream);

      string sLine = "";
      int i = 0;

      while (sLine != null)
      {
        i++;
        sLine = objReader.ReadLine();
        if (sLine != null)
          Console.WriteLine("{0}:{1}", i, sLine);
        htmlString += sLine;
      }
      //Console.ReadLine();
      System.IO.File.WriteAllText(targetFile, htmlString);
    }

    public void ParseWebPageExtractJPG(string html)
    {
      _logger.Info("ParseWebPageExtractJPG method ...");
      HtmlDocument hd = new HtmlDocument();
      hd.LoadHtml(html);
      // LINQ query ... brilliant?  Perhaps!
      var linksOnPage = from imgs in hd.DocumentNode.Descendants()
                        where imgs.Name == "img" 
                        && imgs.Attributes["src"] != null
                        && imgs.Attributes["alt"] != null
                        select new
                        {
                          Url = imgs.Attributes["src"].Value,
                          Alt = imgs.Attributes["alt"].Value
                        };

      foreach (var v in linksOnPage)
      {
        _logger.Info("Url={0}, Alt={1}", v.Url, v.Alt);
      }
    }
  }
}
