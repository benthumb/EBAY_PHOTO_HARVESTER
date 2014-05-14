using System;
//using eBay.Service;
using eBay.Services.Finding;
using eBay.Services;
using System.Xml;
using System.Collections.Generic;
using NLog;

namespace EBAY_PHOTO_HARVESTER
{
  /// <summary>
  /// A sample to show eBay Finding servcie call using the simplied interface provided by the FindingKit.
  /// </summary>
  class Program
  {
    private static readonly Logger logger = LogManager.GetLogger("EBAY_PHOTO_HARVESTER.Program");
    static void Main(string[] args)
    {
      WebPageProcessing wp = new WebPageProcessing(TransactionSettings.BaseDirectory);

      // Init log
      // This sample and the FindingKit use <a href="http://slf.codeplex.com/">Simple Logging Facade(SLF)</a>,
      // Here is a <a href="http://slf.codeplex.com/">good introdution</a> about SLF(for example, supported log levels, how to log to a file)
      //ILogger logger = (ILogger)BitFactoryLogger.CreateSingleFileLogger("C:\\Users\\Paul\\Documents\\pauls_crap\\code\\dot_net_ebay_search\\log.txt"); 

      // Basic service call flow:
      // 1. Setup client configuration
      // 2. Create service client
      // 3. Create outbound request and setup request parameters
      // 4. Call the operation on the service client and receive inbound response
      // 5. Handle response accordingly
      // Handle exception accrodingly if any of the above steps goes wrong.
      try
      {
        ClientConfig config = new ClientConfig();
        // Initialize service end-point configuration
        config.EndPointAddress = TransactionSettings.EndPointAddress;

        // set eBay developer account AppID
        config.ApplicationId = TransactionSettings.ApplicationId;

        // Create a service client
        FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

        // Create request object
        FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();

        // Set request parameters
        request.keywords = TransactionSettings.Keywords;
        PaginationInput pi = new PaginationInput();
        pi.entriesPerPage = TransactionSettings.EntriesPerPage;
        pi.entriesPerPageSpecified = TransactionSettings.EntriesPerPageSpecified;
        request.paginationInput = pi;

        // Call the service
        FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);

        // Show output
        String itemsString = "";
        String rsp = response.ack.ToString();
        logger.Info("Ack = " + response.ack);
        logger.Info("Find " + response.searchResult.count + " items.");
        SearchItem[] items = response.searchResult.item;

        for (int i = 0; i < items.Length; i++)
        {
          itemsString += items[i].title;
          itemsString += '\n';
          itemsString += items[i].viewItemURL;
          itemsString += '\n';
        }
        System.IO.File.WriteAllText(TransactionSettings.TargetFile, itemsString);
        wp.AddSearchResultsToList(TransactionSettings.TargetFile);
      }
      catch (Exception ex)
      {
        // Handle exception if any
        logger.Error(ex);
      }

      Console.WriteLine("Press any key to close the program.");
      Console.ReadKey();

    }
  }
}
